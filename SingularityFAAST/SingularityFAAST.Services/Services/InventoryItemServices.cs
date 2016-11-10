using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SingularityFAAST.Services.Services
{
    public class InventoryItemServices
    {
        //Add getAllInventory() method
        public IList<InventoryItem> GetAllInventory()
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
