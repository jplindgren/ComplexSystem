using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Fulfillment.Domain {
    public class Product {
        public virtual long ProductId { get; set; }
        public virtual long ProductNumber { get; set; }
    }
}
