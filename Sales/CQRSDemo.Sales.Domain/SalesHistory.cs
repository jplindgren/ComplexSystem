﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Sales.Domain {
    public class SalesHistory {
        public Member Member { get; set; }
        public MeasurementPeriod MeasurementPeriod { get; set; }
        public Rebate Rebate { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }// class
}
