using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSDemo.Infrastructure.Repository {
    public class FakeRepository<T> : IRepository<T>{
        private static IList<T> fakeBag;

        public FakeRepository() {
            fakeBag = new List<T>();
        }

        public T Add(T item){
            fakeBag.Add(item);
            return item;
        }
        public IQueryable<T> GetAll() {
            return fakeBag.AsQueryable<T>();
        }
        public void Remove(T item) {
            fakeBag.Remove(item);
        }
        public void SaveChanges() { 
            
        }
    }// class
}
