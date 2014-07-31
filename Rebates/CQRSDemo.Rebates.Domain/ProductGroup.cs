using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class ProductGroup {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }// class
}
