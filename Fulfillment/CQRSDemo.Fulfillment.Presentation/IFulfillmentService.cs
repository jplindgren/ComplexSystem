using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CQRSDemo.Fulfillment.Contract;
using CQRSDemo.Fulfillment.Domain;

namespace CQRSDemo.Fulfillment.Presentation {
    [ServiceContract]
    public interface IFulfillmentService {

        [OperationContract]
        Confirmation PlaceOrder(Order order);
    }// class
}
