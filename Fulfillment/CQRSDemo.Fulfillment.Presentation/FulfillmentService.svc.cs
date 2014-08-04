using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Fulfillment.Contract;
using System.Transactions;
using CQRSDemo.Fulfillment.Application;
using CQRSDemo.Fulfillment.SQL;
using CQRSDemo.Fulfillment.Presentation.MQ;

namespace CQRSDemo.Fulfillment.Presentation {
    public class FulfillmentService : IFulfillmentService {
        private PickListService _pickListService;

        public FulfillmentService() {
            FulfillmentDB.Initialize();
            FulfillmentDB context = new FulfillmentDB();

            _pickListService = new PickListService(context.GetPickListRepository());
            OrderHandler.Instance.Start();
        }

        public void PlaceOrder(Order order) {
            MsmqMessageQueue<Order>.Instance.Send(order);
        }

        public Confirmation CheckOrderStatus(Guid orderId) {
            var pickLists = _pickListService.GetPickLists(orderId);
            if (!pickLists.Any())
                return null;

            return new Confirmation {
                Shipments = pickLists.Select(picklist => new Shipment {
                    ProductId = picklist.Product.ProductId,
                    Quantity = picklist.Quantity,
                    TrackingNumber = "123-456"
                }).ToList()
            };
        }    
    }// class
}
