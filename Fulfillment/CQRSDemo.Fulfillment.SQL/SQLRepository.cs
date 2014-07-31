using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Infrastructure.Repository;
using System.Data.Entity;

namespace CQRSDemo.Fulfillment.SQL {
    public class SQLRepository<T> : IRepository<T> where T : class{
        private DbContext _context;
        private DbSet<T> _dbSet;
        public SQLRepository(DbContext context, DbSet<T> dbSet) {
            _context = context;
            _dbSet = dbSet;
        }

        public T Add(T item) {
            _dbSet.Add(item);
            return item;
        }

        public IQueryable<T> GetAll() {
            return _dbSet.AsQueryable();
        }

        public void Remove(T item) {
            _dbSet.Remove(item);
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }
    }// class
}
