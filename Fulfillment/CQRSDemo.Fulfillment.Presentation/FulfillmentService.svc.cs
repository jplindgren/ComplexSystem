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

namespace CQRSDemo.Fulfillment.Presentation {
    public class FulfillmentService : IFulfillmentService {
        private CustomerService _customerService;
        private ProductService _productService;
        private InventoryAllocationService _inventoryAllocationService;
        private PickListService _pickListService;

        public FulfillmentService() {
            FulfillmentDB.Initialize();
            FulfillmentDB context = new FulfillmentDB();

            _customerService = new CustomerService(context.GetCustomerRepository());
            _productService = new ProductService(context.GetProductRepository());
            _inventoryAllocationService = new InventoryAllocationService(context.GetWarehouseRepository());
            _pickListService = new PickListService(context.GetPickListRepository());

            OrderHandler.Instance.Start();
        }

        public Confirmation PlaceOrder(Order order) {
            IList<PickList> pickLists = ProcessOrder(order);
            return new Confirmation {
                Shipments = pickLists.Select(picklist => new Shipment {
                    ProductId = picklist.Product.ProductId,
                    Quantity = picklist.Quantity,
                    TrackingNumber = "123-456"
                }).ToList()
            };
        }

        private IList<PickList> ProcessOrder(Order order) {
            //using (var scope = new TransactionScope()) {
                Customer customer = _customerService.GetCustomer(order.CustomerName, order.CustomerAddress);

                List<OrderLine> orderLines = order.Lines.Select(line => new OrderLine {
                    Customer = customer,
                    Product = _productService.GetProduct(line.ProductNumber),
                    Quantity = line.Quantity
                }).ToList();

                IList<PickList> pickLists = _inventoryAllocationService.AllocateInventory(order.OrderId, orderLines);

                _pickListService.SavePicklists(pickLists);
                //scope.Complete();
                return pickLists;
            //}
        }    
    }// class
}
