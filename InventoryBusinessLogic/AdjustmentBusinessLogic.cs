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
    }
}
