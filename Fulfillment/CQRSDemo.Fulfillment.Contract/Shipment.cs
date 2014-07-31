using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CQRSDemo.Fulfillment.Contract {
    [DataContract]
    public class Shipment {
        [DataMember]
        public string TrackingNumber { get; set; }

        [DataMember]
        public long ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }// class
}
