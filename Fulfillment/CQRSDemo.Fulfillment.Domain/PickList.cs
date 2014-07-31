using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Fulfillment.Domain {
    public class PickList {
        public virtual long PicklistId { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Product Product { get; set; }
        public virtual int Quantity { get; set; }
    }// class
}
