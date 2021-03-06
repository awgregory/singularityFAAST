﻿//Jon Ebert - 2017
//Inventory Item Services

#region BUGS
//     _______
//    |       |
// ___|       |___
//    |  O O  |      ______
//    |   >   |     | Bugs |
//    |__ 0 __|     | -Jon | <-- Sign/Bug Swatter
// ______| |______  |______|
//|  _         _  |    ||
//| | |   I   | | |    ||
//| | |  Hate | | |___ ||
//| | |  Bugs | |_____|_}
//|_| |       | 
//{_} |_______|
//    |       |
//    |   ||  |
//    |   ||  |
//    |   ||  |
//    |   ||  |
//    |   ||  |
//   [____||____]
//
//Items that need fixin - HIGH PRIORITY
//      1).Delete Item not working:
//              --> gets hung up on "context.SaveChanges();"
//      2). Return Next Inventory Number:
//              --> Needs to be fixed to use ID and not just Database count
//      3). Fix Searching Services
#endregion


using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SingularityFAAST.Core.DataTransferObjects;

namespace SingularityFAAST.Services.Services
{
    public class InventoryItemServices
    {
        #region GetAllInventory
        //gets all inventory items and passes it to the InventoryItemController
        public IList<InventoryItem> GetAllInventory()
        {
            using (var context = new SingularityDBContext())
            {
                var inventory = context.InventoryItems.Where(i => i.IsDeleted == false);
                
                var inventoryList = inventory.ToList();

                return inventoryList;
            }
        }
        #endregion

        #region View Available Inventory
        //returns all inventory items that are currently available
        public IList<InventoryItem> ViewAvailableInv()
        {
            using (var context = new SingularityDBContext())
            {
                IList<InventoryItem> allitems = GetAllInventory(); //gets full list of inventory items from db

                IList<InventoryItem> AvailableItems = allitems.Where(item => item.Availability == true).ToList(); //filters out items which have an availability of True (or 1)

                return AvailableItems;
            }
        }
        #endregion

        #region View Inventory On Loan
        //returns all inventory items that are currently on loan/not available
        public IList<InventoryItem> ViewInvOnLoan()
        {
            //**************
            //NEEDS FIXIN
            //**************

            using (var context = new SingularityDBContext())
            {
                IList<InventoryItem> allitems = GetAllInventory(); //gets full list of inventory items from db

                IList<InventoryItem> ItemsOnLoan = allitems.Where(item => item.Availability == false).ToList(); //filters out items which have an availability of True (or 1)

                return ItemsOnLoan;
            }


        }

        public IList<LoansClientsInventoryDTO> ViewAllItems(object loanNumber)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Return Item Id number for Update Item
        //input: Id # from a specific item number
        //returns InventoryItem object
        public InventoryItem ReturnInventoryItemInteger(int itemId)
        {
            InventoryItem item = GetItemByIdByInt(itemId);

            return (item);
        }
        #endregion

        #region SaveNewItem
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
        #endregion

        #region DeleteItem
        //Deletes Item - used in Update Inventory Items form
        public void DeleteItem(int id)
        {
            using (var context = new SingularityDBContext())
            {
                var item = context.InventoryItems.FirstOrDefault(x => x.InventoryItemId ==id);

                if (item != null)
                {
                    item.IsDeleted = true;

                    context.InventoryItems.Attach(item);
                    var entry = context.Entry(item);
                    entry.State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
        #endregion

        #region EditExistingItem
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
        #endregion

        #region All Search Request code
        
        public IList<InventoryItem> HandlesSearchRequest(SearchRequest searchRequest)
        {
            IList<InventoryItem> filteredItems;

            switch (searchRequest.SearchByType)
            {
                case 1:
                    filteredItems = GetItemByName(searchRequest.SearchBy);
                    break;

                case 2:
                    filteredItems = GetItemByIdByString(searchRequest.SearchBy);
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
        //              -->item with a matching Inventory Id # are returned
        private IList<InventoryItem> GetItemByIdByString(string searchBy)
        {
            IList<InventoryItem> allItems = GetAllInventory();

            IList<InventoryItem> filteredItem = allItems.Where(item =>
                item.InventoryItemId == (Convert.ToInt32(searchBy))).ToList();

            return filteredItem;
        }


        //takes in inventory NUMBER (entered as int)
        //      -->input is converted to int
        //              -->items with a matching Inventory Id # are returned
        private InventoryItem GetItemByIdByInt(int searchBy)
        {
            IList<InventoryItem> allItems = GetAllInventory();

            InventoryItem filteredItem = allItems.FirstOrDefault(item =>
                item.InventoryItemId == (searchBy));

            return filteredItem;
        }
        #endregion

        #region ReturnNextInventoryNumber
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
                //need to add a segment that actually checks the id # of the last item
                //  --> This proposed fix should fix any un-aligned numbers
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
        #endregion

        #region Get Item Categories
        public IList<InventoryItemCategory> GetItemCategories()
        {
            using (var context = new SingularityDBContext())
            {
                var list =  context.InventoryCategories.ToList();

                return list;
            }
        }
        #endregion

        #region Get Loans associated with items
        //public  IList<LoansClientsInventoryDTO> ReturnAssociatedLoanNumber(int id)
        //{
        //    IList<LoansClientsInventoryDTO> allItems = GetAllItems();
        //    IList<LoansClientsInventoryDTO> filteredLoans =
        //        allItems.Where(loan => string.Equals(loan.LoanNumber, loanNumber)).ToList();

        //    return filteredLoans;
        //}
        #endregion
    }
}
