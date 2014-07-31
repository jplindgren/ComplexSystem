using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class Rebate {
        public Method Method { get; set; }
        public string Name { get; set; }
        public MeasuredProductGroup MeasuredProductGroup { get; set; }
        public IEnumerable<Tier> Tiers { get; set; }
    }// class
}
