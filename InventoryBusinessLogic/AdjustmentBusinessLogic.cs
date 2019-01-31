using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryBusinessLogic.Entity;
using InventoryBusinessLogic;

namespace InventoryBusinessLogic
{
    public class AdjustmentBusinessLogic
    {
        Inventory inventory = new Inventory();
        public void addAdjustment(Adjustment adjustment)
        {
            inventory.Adjustment.Add(adjustment);
            inventory.SaveChanges();
        }

        public int generateAdjustmentID()
        {
            try
            {
                return inventory.Adjustment.Max(x => x.AdjustmentID) + 1;
            }
            catch (Exception)
            {
                return 2000;
            }
        }

        public void updateAdjustment(Adjustment adjustment)
        {
            Adjustment update = inventory.Adjustment.Where(x => x.AdjustmentID == adjustment.AdjustmentID).First();
            update.AdjustmentStatus = adjustment.AdjustmentStatus;
            update.TotalPrice = adjustment.TotalPrice;
            update.UserID = adjustment.UserID;
            update.Date = adjustment.Date;
            update.Supervisor = adjustment.Supervisor;
            inventory.SaveChanges();
        }


        public List<Adjustment> getAllAdjustment(string userid)
        {
            return inventory.Adjustment.Where(x => x.Supervisor == userid && (x.AdjustmentStatus).ToUpper().Trim() == "UNAPPROVED").ToList();

        }

        public void ApproveOrRejectRequest(int RequestId, string RequestStatus, string remarks)
        {
            Adjustment request = inventory.Adjustment.Where(x => x.AdjustmentID == RequestId).First();
            List<AdjustmentItem> request1 = inventory.AdjustmentItem.Where(x => x.AdjustmentID == RequestId).ToList<AdjustmentItem>();
            request.AdjustmentStatus = RequestStatus;
            request.Remarks = remarks;
            inventory.SaveChanges();

            string itemid = "";
            int quantity = 0;

            if (RequestStatus == "Approved")
            {
                for (int i = 0; i < request1.Count; i++)
                {
                    itemid = request1[i].ItemID;
                    quantity = Convert.ToInt32(request1[i].Quantity);

                    var q = inventory.Catalogue.Where(x => x.ItemID == itemid).First();

                    if (quantity < 0)
                    {
                        q.Quantity = q.Quantity - Math.Abs(quantity);

                    }
                    else 
                    {
                        q.Quantity = q.Quantity + quantity;
                    }

                        inventory.SaveChanges();

                }
            }

        }


        public List<AdjustmentItem> getAllAdjItems(int adjustmentID)
        {
            return inventory.AdjustmentItem.Where(x => x.AdjustmentID == adjustmentID).ToList();

        }

        public List<Adjustment> getAllAdjustmentList(string userid)
        {
            return inventory.Adjustment.Where(x => x.UserID == userid).ToList();

        }





    }
}
