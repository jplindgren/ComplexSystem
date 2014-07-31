using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Infrastructure.Repository {
    public interface IRepository<T> {
        T Add(T item);
        IQueryable<T> GetAll();
        void Remove(T item);
        void SaveChanges();
    }// class
}
