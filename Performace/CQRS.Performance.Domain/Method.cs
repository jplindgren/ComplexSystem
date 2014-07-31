using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.Performance.Domain {
    public abstract class Method {
        public abstract int CalculatePercent();
        public abstract int CalculateUnits();
    }// class
}
