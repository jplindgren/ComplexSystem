using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;

namespace CQRSDemo.Fulfillment.Application {
    public class CustomerService {
        private IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository) {
            _customerRepository = customerRepository;
        }

        public Customer GetCustomer(string customerName, string customerAddress) {
            var customer = _customerRepository.GetAll().Where(c => c.Name == customerName && c.ShippingAddress == customerAddress).FirstOrDefault();
            return customer;
        }
    }//class
}
