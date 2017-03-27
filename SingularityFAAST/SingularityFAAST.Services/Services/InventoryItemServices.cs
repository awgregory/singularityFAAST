using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.SearchRequests;
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
        
        //
        //All Search Request code
        //
        //public IList<InventoryItem> HandlesSearchRequest(SearchRequest searchRequest)
        //{
        //    IList<InventoryItem> filteredItems;

        //    switch (searchRequest.SearchByType)
        //    {
        //        case 1:
        //            filteredItems = GetItemByName(searchRequest.SearchBy);
        //            break;

        //        case 2:
        //            filteredItems = GetItemByInventoryNumber(searchRequest.SearchBy);
        //            break;

        //        default:
        //            filteredItems = new List<InventoryItem>(0);
        //            break;

        //    }
        //    return filteredItems;
        //}


        //private IList<InventoryItem> GetItemByName(string searchBy)
        //{
        //    IList<Client> allItems = GetAllClients();

        //    IList<Client> filteredItems = allItems.Where(client =>
        //        string.Equals(client.FirstName, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();

        //    return filteredItems;
        //}



        //public IList<InventoryItem> GetItemByInventoryNumber(string searchBy)
        //{
        //    //IList<InventoryItem> allItems = GetAllItems();

        //    //IList<InventoryItem> filteredItems = allItems.Where(client =>
        //    //    client.ClientID == (Convert.ToInt32(searchBy))).ToList();

        //    return filteredItems;
        //}
    }
}
