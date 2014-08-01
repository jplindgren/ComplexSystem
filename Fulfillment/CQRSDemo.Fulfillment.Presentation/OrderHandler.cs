using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CQRSDemo.Fulfillment.Application;
using System.Threading;
using CQRSDemo.Fulfillment.SQL;
using CQRSDemo.Fulfillment.Contract;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Fulfillment.Presentation.MQ;

namespace CQRSDemo.Fulfillment.Presentation {
    public class OrderHandler {
        private static OrderHandler _instance = new OrderHandler();

        public static OrderHandler Instance {
            get { 
                return _instance; 
            }
        }

        private ManualResetEvent _stop = new ManualResetEvent(true);
        private Thread _thread;
        private IMessageQueue<Order> _messageQueue;

        private OrderHandler() {
            FulfillmentDB.Initialize();

            _messageQueue = MsmqMessageQueue<Order>.Instance;

            _thread = new Thread(ThreadProc);
            _thread.Name = "OrderHandler";
        }

        public void Start() {
            if (_thread.ThreadState == ThreadState.Unstarted)
                _thread.Start();
        }

        public void Stop() {
            _stop.Set();
            _thread.Join();
        }


        public void ThreadProc(object o) {
            Order order;
            while (!_stop.WaitOne(0)) {
                if (_messageQueue.TryReceive(out order)){
                    using (OrderProcessor processor = new OrderProcessor()){
                        processor.ProcessOrder(order);
                    } 
                }
                    
            }
        }
    }// class
}