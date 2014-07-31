using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class MeasuredProductGroup {
        public string Name { get; set; }
        public MeasuredDrugClass MeasuredDrugClass { get; set; }
        public IEnumerable<MeasuredProduct> Products { get; set; }
    }// class
}
