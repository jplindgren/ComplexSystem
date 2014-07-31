using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CQRSDemo.Fulfillment.Contract {
    [DataContract]
    public class Line {
        [DataMember]
        public long ProductNumber { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }// class
}
