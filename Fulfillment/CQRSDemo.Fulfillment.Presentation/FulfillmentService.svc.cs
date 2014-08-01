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
        private CustomerService _customerService;
        private ProductService _productService;
        private InventoryAllocationService _inventoryAllocationService;
        private PickListService _pickListService;
        private IMessageQueue<Order> _messageQueue;

        public FulfillmentService() {
            FulfillmentDB.Initialize();
            FulfillmentDB context = new FulfillmentDB();

            _customerService = new CustomerService(context.GetCustomerRepository());
            _productService = new ProductService(context.GetProductRepository());
            _inventoryAllocationService = new InventoryAllocationService(context.GetWarehouseRepository());
            _pickListService = new PickListService(context.GetPickListRepository());

            _messageQueue = MsmqMessageQueue<Order>.Instance;
            OrderHandler.Instance.Start();
        }

        public void PlaceOrder(Order order) {
            _messageQueue.Send(order);            
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
