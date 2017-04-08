﻿//Jon Ebert -2017
//Inventory Item Controller

using System.Collections.Generic;
using SingularityFAAST.Core.SearchRequests;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Services.Services;
using System;
using SingularityFAAST.Core.ViewModels;

namespace SingularityFAAST.WebUI.Controllers
{
    [Authorize]
    public class InventoryItemController : Controller
    {
        private readonly InventoryItemServices itemServices = new InventoryItemServices();


        #region IndexInventory
        public ActionResult IndexInventory()
        {
            var model = itemServices.GetAllInventory();
            return View(model);
        }
        #endregion

        #region All AVAILABLE Inventory
        public ActionResult ViewAllAvailableInv()
        {
            var model = itemServices.ViewAvailableInv();
            return View(model);
        }
        #endregion

        #region All Inventory ON LOAN
        public ActionResult ViewAllOnLoanInv()
        {
            var services = new InventoryItemServices();
            var model = services.ViewInvOnLoan();
            return View(model);
        }
        #endregion

        #region New Item Creation methods
        //New Item Creation
        //pushes next Inventory Item number into form
        [HttpGet]
        public ActionResult NewInventoryItem()
        {
            int itemCount = itemServices.ReturnNextInventoryNumber();
            IList<InventoryItem> items = itemServices.GetAllInventory();

            var model = new NewInventoryItemViewModel()
            {
                InventoryItems = items,
                ReturnNextInventoryNumber = itemCount
            };

            return View(model);//returns both the ItemCount and the IList<>
            //return View(model);
        }

        //Collects data from New Inventory Item form
        [HttpPost]
        public RedirectToRouteResult NewInventoryItem(InventoryItem item)
        {
            if (ModelState.IsValid)//Adding Validation to the Submission on a new inventory item
            {
             itemServices.SaveNewItem(item);

             return RedirectToAction("IndexInventory", "InventoryItem");
            }
            else
            {
                ModelState.AddModelError("", "Please Enter All Required Information");//the returned error message
            }

            return RedirectToAction("NewInventoryItem"); //returns the user to the view with the item
        }

        #endregion

        #region UpdateInventoryItem Methods

        //update inventory item view
        [HttpGet]
        public ActionResult UpdateInventoryItem(int id)
        {
            var item = itemServices.ReturnInventoryItemInteger(id); //create new service for update item that passes 
            var model = new UpdateInventoryItemViewModel()//new view model specific to the update item form
            {
                InventoryItems = item,
                NextInventoryNumber = id
            };
            return View(model);
        }


        //post method for updating item after user makes changes
        //  -->Redirects user to InventoryIndex
        [HttpPost]
        public RedirectToRouteResult UpdateInventoryItem(InventoryItem item) //needs modelState validation
        {
            if (ModelState.IsValid)
            {
              itemServices.EditExistingItem(item);

            return RedirectToAction("IndexInventory", "InventoryItem");
            }
            else
            {
                ModelState.AddModelError("", "Please Enter All Required Information");//the returned error message
            }

          return RedirectToAction("UpdateInventoryItem"); //returns the user to the view with the item
        }

        //post method for updating item after user makes changes
        //  -->Redirects user to InventoryIndex
        [HttpPost]
        public RedirectToRouteResult DeleteInventoryItem(InventoryItem item) //needs modelState validation
        {
                itemServices.DeleteItem(item);

                return RedirectToAction("IndexInventory", "InventoryItem");
        }
        #endregion

        #region Search Requests 
        //Returns Inventory records that match search criteria
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {

            IList<InventoryItem> model = itemServices.HandlesSearchRequest(searchRequest);

            return View(model);
        }
        #endregion
    }
}