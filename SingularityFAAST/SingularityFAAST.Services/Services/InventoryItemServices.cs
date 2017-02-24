using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SingularityFAAST.Services.Services
{
    public class InventoryItemServices
    {
        //gets all inventory items and passes it to the InventoryItemController
        public IList<InventoryItem> GetAllInventory()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems;

                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }

        //add new inventory item
        public void SaveNewItem(InventoryItem item)
        {
            using (var context = new SingularityDBContext())
            {
                item.DatePurchased = DateTime.Now;  // Manipulating the item object is done before saving to Db

                context.InventoryItems.Add(item);

                context.SaveChanges();
             }
         }

        //edit existing item
        public void EditExistingItem(InventoryItem item)
        {
            using (var context = new SingularityDBContext())
            {
                context.InventoryItems.Attach(item);
                var entry = context.Entry(item);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        //returns all inventory items that 
        public IList<InventoryItem> ViewAvailableInv()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems;

                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }


        public IList<InventoryItem> ViewInvOnLoan()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems;

                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }
    }
}
