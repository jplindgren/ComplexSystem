using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CQRSDemo.Fulfillment.Application;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;

namespace CQRSDemo.Fulfillment.Test {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class InventoryAllocationServiceTest {
        private InventoryAllocationService _service;
        private FakeRepository<Warehouse> _warehouses;

        private Warehouse warehouse1;
        private Product product;
        private Customer customer;

        [TestInitialize()]
        public void Initialize() {
            _warehouses = new FakeRepository<Warehouse>();
            warehouse1 = new Warehouse { Name="Hangar 18" };
            product = new Product{ ProductId=11190 };

            warehouse1.Inventory = new List<Inventory>();
            warehouse1.Inventory.Add(new Inventory{
                Product = product,
                QuantityOnHand = 7
            });

            _warehouses.Add(warehouse1);

            customer = new Customer(){
                Name = "Cliente",
                ShippingAddress = "Rua Adolfo Junior"
            };

            _service = new InventoryAllocationService(_warehouses);
        }
        
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        [TestMethod]
        public void CanAllocateFromOneWarehouse() {
            IList<OrderLine> orderLines = new List<OrderLine>{
                new OrderLine{
                    Customer = customer,
                    Product = product,
                    Quantity = 3
                }
            };

            IList<PickList> picklists = _service.AllocateInventory(Guid.NewGuid(), orderLines);
            Assert.AreEqual(1, picklists.Count);
            Assert.AreEqual(11190, picklists[0].Product.ProductId);
            Assert.AreEqual(3, picklists[0].Quantity);
        }
    }// class
}
