//Jon Ebert - 2017
//Inventory Item Services

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


        //add new inventory item-------------------------------------
        public void SaveNewItem(InventoryItem item)
        {
            using (var context = new SingularityDBContext())
            {
                item.DatePurchased = DateTime.Now;  // Manipulating the item object is done before saving to Db

                context.InventoryItems.Add(item);

                context.SaveChanges();
             }
         }
        //-----------------------------------------------------------

        //Deletes Item - used in Update Inventory Items form <<<<<<<<<<<<
        public void DeleteItem(InventoryItem item)
        {
            using (var context = new SingularityDBContext())
            {
                context.InventoryItems.Attach(item);

                var entry = context.Entry(item);

                entry.State = EntityState.Deleted;

                context.SaveChanges();
            }
        }
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


        //edit existing item>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
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
        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


        //returns all inventory items that are currently available
        public IList<InventoryItem> ViewAvailableInv()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems;

                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }

        //returns all inventory items that are currently on loan/not available
        public IList<InventoryItem> ViewInvOnLoan()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems;

                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }

        //**********************************************************
        //All Search Request code
        //**********************************************************
        public IList<InventoryItem> HandlesSearchRequest(SearchRequest searchRequest)
        {
            IList<InventoryItem> filteredItems;

            switch (searchRequest.SearchByType)
            {
                case 1:
                    filteredItems = GetItemByName(searchRequest.SearchBy);
                    break;

                case 2:
                    filteredItems = GetItemByInventoryNumber(searchRequest.SearchBy);
                    break;

                default:
                    filteredItems = new List<InventoryItem>(0);
                    break;

            }
            return filteredItems;
        }

        //takes in inventory NAME
        //      -->items with a matching Inventory name are returned
        private IList<InventoryItem> GetItemByName(string searchBy)
        {
            IList<InventoryItem> allItems = GetAllInventory();

            IList<InventoryItem> filteredItems = allItems.Where(item =>
                string.Equals(item.ItemName, searchBy, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredItems;
        }


        //takes in inventory NUMBER (entered as string)
        //      -->input is converted to int
        //              -->items with a matching Inventory Id # are returned
        private IList<InventoryItem> GetItemByInventoryNumber(string searchBy)
        {
            IList<InventoryItem> allItems = GetAllInventory();

            IList<InventoryItem> filteredItems = allItems.Where(item =>
                item.InventoryItemId == (Convert.ToInt32(searchBy))).ToList();

            return filteredItems;
        }
        //*************************************************************

        
        //Used for [HttpGet] method "NewInventoryItem" in Inventory Controller
            //Return Greatest Inventory Number to Display in "NewInventoryItem"...
            //...as the next inventory number to be assigned to the new item being created
        public int ReturnNextInventoryNumber()
        {
            using (var context = new SingularityDBContext())
            {
                var items = context.InventoryItems;

                IList<InventoryItem> allItems = GetAllInventory();
                var itemCount = allItems.Count;

                if(allItems.Count > 0)
                {
                    var InventoryNumber = itemCount + 1;
                    return InventoryNumber;
                }
                else
                {
                    itemCount = 1;
                    var InventoryNumber = itemCount;
                    return InventoryNumber;
                }
                
            }
        }
    }
}
