using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class MeasuredDrugClass {
        public string Name { get; set; }
        public IEnumerable<MeasuredProductGroup> MeasuredProductGroups { get; set; }
    }
}
