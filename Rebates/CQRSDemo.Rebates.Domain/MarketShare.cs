using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Rebates.Domain {
    public class MarketShare : Method{
        public override int CalculatePercent() {
            throw new NotImplementedException();
        }

        public override int CalculateUnits() {
            throw new NotImplementedException();
        }
    }// class
}
