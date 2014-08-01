using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;
using System.Transactions;

namespace CQRSDemo.Fulfillment.Presentation.MQ {
    public class MsmqMessageQueue<T> : IMessageQueue<T>{
        private string _path;
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(30.0);
        private XmlMessageFormatter Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        private static MsmqMessageQueue<T> _instance = new MsmqMessageQueue<T>();

        public MsmqMessageQueue() {
            _path = @".\private$\orders";
            if (!MessageQueue.Exists(_path)){
                MessageQueue.Create(_path, transactional: true);
            }
        }

        public static IMessageQueue<T> Instance {
            get {
                if (_instance == null) {
                    _instance = new MsmqMessageQueue<T>();
                }
                return _instance; 
            }
        }

        public void Send(T message) {
            //using (var scope = new TransactionScope()) {
            using (var queue = new MessageQueue(_path)) {
                queue.DefaultPropertiesToSend.Recoverable = true;
                queue.Formatter = Formatter;
                queue.Send(message, MessageQueueTransactionType.Automatic);
            }
            //}
        }

        public bool TryReceive(out T message) {
            try {
                using (var queue = new MessageQueue(_path)) {
                    var msmqMessage = queue.Receive(Timeout, MessageQueueTransactionType.Automatic);
                    msmqMessage.Formatter = Formatter;
                    message = (T)msmqMessage.Body;
                    return true;
                }
            } catch (Exception ex) {
                message = default(T);
                return false;
            }
        }
    }// class
}