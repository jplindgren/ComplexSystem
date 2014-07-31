using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CQRSDemo.Fulfillment.Presentation {
    public class OrderHandler {
        private static OrderHandler _instance;

        public static OrderHandler Instance {
            get { 
                if (_instance == null){
                    _instance = new OrderHandler();
                }
                return _instance; 
            }
        }

        public void Start() { }

    }// class
}