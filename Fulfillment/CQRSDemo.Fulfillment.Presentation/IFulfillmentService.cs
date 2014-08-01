using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CQRSDemo.Fulfillment.Contract;

namespace CQRSDemo.Fulfillment.Presentation {
    // Ao invés de algo síncrono, que nos traz problemas de concorrência, usaremos uma comunicação assíncrona
    //usaremos Command Query Segregation para começar. 
    //Métodos que alteram estados (commands) não retornam nada
    //Apenas métodos quem realizam buscas(queries) podem retornar dados
    [ServiceContract]
    public interface IFulfillmentService {
        [OperationContract]
        void PlaceOrder(Order order);

        [OperationContract]
        Confirmation CheckOrderStatus(Guid orderId);
    }// class
}
