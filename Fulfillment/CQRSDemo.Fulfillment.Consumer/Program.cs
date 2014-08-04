using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Consumer.FulfillmentService;

namespace CQRSDemo.Fulfillment.Consumer {
    class Program {
        static void Main(string[] args) {
            List<Guid> orderIds = new List<Guid>();
            FulfillmentServiceClient client = new FulfillmentServiceClient();

            Line[] lines = {
                    new Line{
                        ProductNumber = 11190,
                        Quantity = 2
                    }       
                };

            var order = new Order {
                CustomerName = "Sherlock Holmes",
                CustomerAddress = "221B Baker Street",
                Lines = lines
            };

            while (true) {
                try {
                    order.OrderId = Guid.NewGuid();
                    PlaceOrder(client, order);
                    orderIds.Add(order.OrderId);

                    var processedOrderIds = orderIds.Where(x => CheckOrderStatus(client, x)).ToList();
                    orderIds.RemoveAll(id => processedOrderIds.Contains(id));
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void PlaceOrder(FulfillmentServiceClient client, Order order) {
            client.PlaceOrder(order);
        }

        private static bool CheckOrderStatus(FulfillmentServiceClient client, Guid orderId) {
            var confirmation = client.CheckOrderStatus(orderId);

            if (confirmation == null)
                return false;

            PrintConfirmation(confirmation);
            return true;
        }

        private static void PrintConfirmation(Confirmation confirmation) {
            foreach (var shipment in confirmation.Shipments){
                Console.WriteLine(string.Format("Ordem criada para o produto {0}, quantidade = {1} tracking number = {2}", shipment.ProductId, shipment.Quantity, shipment.TrackingNumber));    
            }
            
        }// main

    }// class
}
