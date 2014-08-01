using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CQRSDemo.Fulfillment.Application;
using CQRSDemo.Fulfillment.SQL;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Fulfillment.Contract;

namespace CQRSDemo.Fulfillment.Presentation {
    public class OrderProcessor : IDisposable{
        private FulfillmentDB _context;
        private CustomerService _customerService;
        private ProductService _productService;
        private InventoryAllocationService _inventoryAllocationService;
        private PickListService _pickListService;

        private Random _databaseError = new Random();

        public OrderProcessor() {
            _context = new FulfillmentDB();
            _customerService = new CustomerService(_context.GetCustomerRepository());
            _productService = new ProductService(_context.GetProductRepository());
            _inventoryAllocationService = new InventoryAllocationService(_context.GetWarehouseRepository());
            _pickListService = new PickListService(_context.GetPickListRepository());
        }

        public void Dispose() {
            _context.Dispose();
        }

        public  void ProcessOrder(Order order) {
            if (_pickListService.GetPickLists(order.OrderId).Any())
                return;

            Customer customer = _customerService.GetCustomer(order.CustomerName, order.CustomerAddress);

            List<OrderLine> orderLines = order.Lines.Select(line => new OrderLine {
                Customer = customer,
                Product = _productService.GetProduct(line.ProductNumber),
                Quantity = line.Quantity
            }).ToList();

            IList<PickList> pickLists = _inventoryAllocationService.AllocateInventory(order.OrderId, orderLines);

            _pickListService.SavePicklists(pickLists);
        }
    }// class
}