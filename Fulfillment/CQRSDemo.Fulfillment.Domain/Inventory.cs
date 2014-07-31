using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CQRSDemo.Fulfillment.Domain {
    public class Inventory {
        public long QuantityOnHand { get; set; }

        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }
    }// class
}
