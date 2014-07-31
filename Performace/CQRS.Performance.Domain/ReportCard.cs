using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.Performance.Domain {
    public class ReportCard {
        public int TotalSalesUnits { get; set; }
        public int UnitsToNextLevel { get; set; }
        public Rebate Rebate { get; set; }
        public Member Member { get; set; }
        public MeasurementPeriod MeasurementPeriod { get; set; }
        public Award Award { get; set; } 
    }// class
}
