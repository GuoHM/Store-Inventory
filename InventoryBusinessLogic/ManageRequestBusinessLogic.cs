using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;
namespace InventoryBusinessLogic
{
    public class ManageRequestBusinessLogic
    {
        Inventory inventory = new Inventory();

        public void ApproveOrRejectRequest(string RequestId, string RequestStatus,string remarks)
        {

            int requestID = Convert.ToInt32(RequestId);
            var request = inventory.Request.Where(x => x.RequestID == requestID).ToList();
            foreach (var req in request)
            {
                req.RequestStatus = RequestStatus;
                req.Remarks = remarks;
                if (RequestStatus.ToUpper().Trim() == "APPROVED")
                {
                    var order = inventory.Order.Where(x => x.OrderID == req.OrderID).First();
                    order.OrderStatus = RequestStatus;
                }
                inventory.SaveChanges();
            }
          
            //var order = inventory.Order.Where(x => x.OrderID == RequestId).First();
            //order.OrderStatus = RequestStatus;
            //inventory.SaveChanges();
        }

        public List<Request> GetAllRequests()
        {
            List<Request> req = inventory.Request.Where(x => (x.RequestStatus).ToUpper().Trim() == "APPROVED").ToList();
            List<Request> orders = new List<Request>();


            foreach (Request req1 in req)
            {
                if (!orders.Any(x => x.OrderID == req1.OrderID && x.UserID == req1.UserID))
                {

                    orders.Add(req1);
                }
            }
            //if(!order.Any(x=>x.OrderID==req1.OrderID && x.Request.))
            //}
            return orders;
            //return inventory.Request.ToList();

        }



        public List<Request> GetRetrievalItems()
        {
            //var orders = inventory.Order.Select(x => x.OrderID).Distinct().ToList();
            //List<Request> requests = new List<Request>();
            //foreach(var order in orders)
            //{
            //  requests.AddRange(GetRequestByOrderId(order));
            //}


            return inventory.Request.Where(x => ((x.RequestStatus).ToUpper().Trim() == "APPROVED") && (x.Needed != x.Actual)).ToList();
        }

        
        public Request GetRequestById(int requestId)
        {
            try
            {
                return inventory.Request.Where(x => x.RequestID == requestId).First();
            } catch (Exception)
            {
                return null;
            }
        }

        public List<Request> GetRequestByOrderId(string OrderId)
        {
            try
            {
                return inventory.Request.Where(x => x.OrderID == OrderId &&x.RequestStatus.ToUpper().Trim()=="APPROVED" && x.Actual != x.Needed).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void addRequest(Request request)
        {
            inventory.Request.Add(request);
            inventory.SaveChanges();
        }

        public Request GetRequestsByOrderAndItem(string orderId, string itemId)
        {
            try
            {
                return inventory.Request.Where(x => x.OrderID == orderId && x.ItemID == itemId).First();
            } catch (Exception)
            {
                return null;
            }
        }

        public void UpdateRequest(Request request)
        {
            Request update = inventory.Request.Where(x => x.RequestID == request.RequestID).First();
            update.Actual = request.Actual;
            update.ItemID = request.ItemID;
            update.OrderID = request.OrderID;
            update.Remarks = request.Remarks;
            update.RequestDate = request.RequestDate;
            update.RequestStatus = request.RequestStatus;
            update.Needed = request.Needed;
            inventory.SaveChanges();
        }

       
        public List<Request> getAllStationeryRequest(string userId)
        {
            return inventory.Request.Where(x => x.UserID == userId).GroupBy(x => new { x.OrderID, x.RequestStatus }).Select(x => x.FirstOrDefault()).ToList();

        }
      
        public List<Request> GetAllApprovalPendingRequests(string userid)
        {
            AspNetUsers dept = inventory.AspNetUsers.Where(x => x.Id == userid).First();
            List<Request> req = inventory.Request.Where(x => (x.RequestStatus).ToUpper().Trim() == "UNAPPROVED" && x.OrderID.Substring(0,4) ==dept.DepartmentID.Substring(0,4)).ToList();
            List<Request> orders = new List<Request>();


            foreach (Request req1 in req)
            {
                if (!orders.Any(x => x.OrderID == req1.OrderID && x.UserID == req1.UserID))
                {

                    orders.Add(req1);
                }
            }

            
            return orders;
           

        }

        public List<Request> GetRequestByOrderIdUserId(string OrderId,string UserName)
        {
            try
            {
                return inventory.Request.Where(x => x.OrderID == OrderId && x.AspNetUsers.UserName.Trim()==UserName.Trim() && x.RequestStatus.ToUpper().Trim()=="UNAPPROVED").ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Request> getStationaryOrderByID(string orderId, string userid, string status)
        {
            return inventory.Request.Where(x => x.OrderID == orderId && x.UserID==userid && x.RequestStatus.Trim()==status.Trim()).ToList();

        }

      

      



    }


    public class RequestList
    {
        public string OrderID { get; set; }
        public string RequestDate { get; set; }
        public string UserID { get; set; }
        public string RequestStatus { get; set; }
    }
}

