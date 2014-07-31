using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Sales.Domain {
    public class Product {
        public long Id { get; set; }
        public string Name { get; set; }
        public Rebate Rebate { get; set; }
    }// class
}
