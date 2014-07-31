using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;

namespace CQRSDemo.Fulfillment.Application {
    public class InventoryAllocationService {
        private IRepository<Warehouse> _warehouseRepository;

        public InventoryAllocationService(IRepository<Warehouse> warehouseRepository) {
            _warehouseRepository = warehouseRepository;
        }

        public IList<PickList> AllocateInventory(Guid orderId, IList<OrderLine> orderLines) {
            List<PickList> pickLists = new List<PickList>();
            foreach (var orderLine in orderLines){
                Warehouse warehouse = LocateProduct(orderLine.Product, orderLine.Quantity);

                if (warehouse != null) {
                    PickList picklist = PickProduct(orderId, orderLine.Product, orderLine.Quantity, warehouse);
                    pickLists.Add(picklist);
                }
            }
            return pickLists;
        }

        private Warehouse LocateProduct(Product product, int quantity) {
            IEnumerable<Warehouse> warehouses = _warehouseRepository.GetAll();
            var selectedWarehouses = warehouses.Where(x =>
                    x.Inventory.Any(i => i.Product == product && i.QuantityOnHand >= quantity))
                    .FirstOrDefault();
            return selectedWarehouses;
        }

        private PickList PickProduct(Guid orderId, Product product, int quantity, Warehouse warehouse) {
            var inventory = warehouse.Inventory.Single(x => x.Product == product);
            inventory.QuantityOnHand = inventory.QuantityOnHand - quantity;
            _warehouseRepository.SaveChanges();

            return new PickList() {
                OrderId = orderId,
                Product = product,
                Warehouse = warehouse,
                Quantity = quantity
            };
        }
    }//class
}
