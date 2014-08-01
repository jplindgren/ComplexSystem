using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSDemo.Fulfillment.Domain;
using CQRSDemo.Infrastructure.Repository;

namespace CQRSDemo.Fulfillment.Application {
    public class PickListService {
        private IRepository<PickList> _pickListRepository;

        public PickListService(IRepository<PickList> pickListRepository) {
            _pickListRepository = pickListRepository;
        }

        public void SavePicklists(IList<PickList> pickLists) {
            foreach (var picklist in pickLists) { 
                _pickListRepository.Add(picklist);
            }
            _pickListRepository.SaveChanges();
        }

        public IList<PickList> GetPickLists(Guid orderId) {
            return _pickListRepository.GetAll().Where(x => x.OrderId == orderId).ToList();
        }
    }//class
}
