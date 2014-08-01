using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Threading;

namespace CQRSDemo.Fulfillment.Presentation.MQ {
    public class MemoryMessageQueue<T> : CQRSDemo.Fulfillment.Presentation.MQ.IMessageQueue<T> {
        private static MemoryMessageQueue<T> _instance;

        public static MemoryMessageQueue<T> Instance {
            get {
                if (_instance == null) {
                    _instance = new MemoryMessageQueue<T>();
                }
                return _instance;
            }
        }

        private Queue<T> _messages = new Queue<T>();

        public void Send(T message) {
            lock (this) {
                _messages.Enqueue(message);
                Monitor.PulseAll(this);
            }
        }

        public bool TryReceive(out T message) {
            lock (this) {
                message = default(T);
                //if (_messages.Count == 0)
                    //Monitor.Wait(this);
                if (_messages.Count != 0)
                    message = _messages.Dequeue();
                return message != null;
            }
        }
    }// class
}