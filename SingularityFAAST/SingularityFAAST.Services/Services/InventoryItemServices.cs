﻿using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.Entities;
using System.Collections.Generic;
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
