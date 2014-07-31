using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class DrugClass {
        public string Name { get; set; }
        public IEnumerable<ProductGroup> ProductGroup { get; set; }
    }// class
}
