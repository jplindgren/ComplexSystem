using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Fulfillment.Domain {
    public class Warehouse {
        public virtual long WarehouseId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Inventory> Inventory { get; set; }
    }// class
}
