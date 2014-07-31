using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CQRSDemo.Fulfillment.Contract {
    [DataContract]
    public class Order {
        [DataMember]
        public Guid OrderId { get;set;}

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public List<Line> Lines { get; set; }
    }// class
}
