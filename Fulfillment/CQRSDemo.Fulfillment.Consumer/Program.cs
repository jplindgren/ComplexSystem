using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Presentation;
using CQRSDemo.Fulfillment.Consumer.FulfillmentService;

namespace CQRSDemo.Fulfillment.Consumer {
    class Program {
        static void Main(string[] args) {
            IList<Guid> orderIds = new List<Guid>();
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
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static void PlaceOrder(FulfillmentServiceClient client, Order order) {
            var confirmation = client.PlaceOrder(order);
            PrintConfirmation(confirmation);
        }

        private static void PrintConfirmation(Confirmation confirmation) {
            foreach (var shipment in confirmation.Shipments){
                Console.WriteLine(string.Format("Ordem criada para o produto {0}, quantidade = {1} tracking number = {2}", shipment.ProductId, shipment.Quantity, shipment.TrackingNumber));    
            }
            
        }// main

    }// class
}
