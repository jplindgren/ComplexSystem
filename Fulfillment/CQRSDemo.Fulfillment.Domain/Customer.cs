using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Fulfillment.Domain {
    public class Customer {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShippingAddress { get; set; }
    }
}
