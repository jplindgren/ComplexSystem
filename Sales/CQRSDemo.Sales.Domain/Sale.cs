using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Sales.Domain {
    public class Sale {
        public DateTime Date { get; set; }
        public int Units { get; set; }
        public Product Product { get; set; }
    }// class
}
