using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;

namespace CQRSDemo.Fulfillment.Application {
    public class ProductService {
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository) {
            _productRepository = productRepository;
        }

        public Product GetProduct(long productNumber) {
            var product = _productRepository.GetAll().Where(c => c.ProductNumber == productNumber).FirstOrDefault();
            return product;
        }
    }//class
}
