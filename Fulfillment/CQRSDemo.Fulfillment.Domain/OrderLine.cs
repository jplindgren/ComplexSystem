using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Fulfillment.Domain {
    public class OrderLine {
        public virtual int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }// class
}
