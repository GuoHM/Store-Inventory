using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;
namespace InventoryBusinessLogic
{
    class ManageRequestBusinessLogic
    {
        Inventory inventory = new Inventory();

        public void ApproveOrRejectRequest(string RequestId, string RequestStatus)
        {
            var request = inventory.Request.Where(x => x.RequestID == RequestId).First();
            request.RequestStatus = RequestStatus;
            inventory.SaveChanges();
        }

        public List<Request> GetAllRequests()
        {
            return inventory.Request.ToList();
        }

    }
}

