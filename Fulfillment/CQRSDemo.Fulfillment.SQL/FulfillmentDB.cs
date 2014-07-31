using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CQRSDemo.Fulfillment.SQL {
    public class FulfillmentDB : DbContext{
        public FulfillmentDB()
            : base("DefaultConnection") {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public static void Initialize() { 
            Database.SetInitializer<FulfillmentDB>(new DropCreateDatabaseIfModelChanges<FulfillmentDB>());
        }

        public DbSet<Customer> Customers {get;set;}
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PickList> PickLists{ get; set; }

        public IRepository<Customer> GetCustomerRepository() {
            return new SQLRepository<Customer>(this, Customers);
        }

        public IRepository<Warehouse> GetWarehouseRepository() {
            return new SQLRepository<Warehouse>(this, Warehouses);
        }

        public IRepository<Product> GetProductRepository() {
            return new SQLRepository<Product>(this, Products);
        }

        public IRepository<PickList> GetPickListRepository() {
            return new SQLRepository<PickList>(this, PickLists);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Inventory>().HasKey(d => new { d.WarehouseId, d.ProductId }); 
        }
    }// class
}
