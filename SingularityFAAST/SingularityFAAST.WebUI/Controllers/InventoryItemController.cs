//Jon Ebert - 2017
//Inventory Item Controller

using System.Collections.Generic;
using SingularityFAAST.Core.SearchRequests;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Services.Services;
using System.Linq;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Core.ViewModels;

namespace SingularityFAAST.WebUI.Controllers
{
    [Authorize]
    public class InventoryItemController : Controller
    {
        private readonly InventoryItemServices _itemServices = new InventoryItemServices();

        private readonly int _pageSize = 25;


        #region IndexInventory
        public ActionResult IndexInventory(int page = 1)
        {
            var allInventory = _itemServices.GetAllInventory();

            var list = allInventory
                .OrderBy(ii => ii.InventoryItemId)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize);

            var viewModel = new ItemIndexViewModel
            {
                InventoryItems = list,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = allInventory.Count
                }
            };

            return View(viewModel);
        }
        #endregion

        #region All AVAILABLE Inventory
        public ActionResult ViewAllAvailableInv(int page = 1)
        {
            var model = _itemServices.ViewAvailableInv();

            var list = model
                .OrderBy(ii => ii.InventoryItemId)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize);

            var viewModel = new ItemIndexViewModel
            {
                InventoryItems = list,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = model.Count
                }
            };

            return View(viewModel);
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
            int itemCount = _itemServices.ReturnNextInventoryNumber();
            IList<InventoryItem> items = _itemServices.GetAllInventory();

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
        public ActionResult NewInventoryItem(InventoryItem item)
        {
            if (ModelState.IsValid)//Adding Validation to the Submission on a new inventory item
            {
                _itemServices.SaveNewItem(item);

                return RedirectToAction("IndexInventory", "InventoryItem");
            }
            else
            {
                ModelState.AddModelError("", "Please Enter All Required Information");//the returned error message
            }

            //return RedirectToAction("NewInventoryItem"); //returns the user to the view with the item
            return View(new NewInventoryItemViewModel() { });
        }

        #endregion

        #region UpdateInventoryItem Methods

        //update inventory item view
        [HttpGet]
        public ActionResult UpdateInventoryItem(int id)
        {
            var item = _itemServices.ReturnInventoryItemInteger(id); //create new service for update item that passes 

            var inventoryItemCategories = _itemServices.GetItemCategories();

            var model = new UpdateInventoryItemViewModel()//new view model specific to the update item form
            {
                InventoryItem = item,
                NextInventoryNumber = id,
                InventoryItemCategories = inventoryItemCategories
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
                _itemServices.EditExistingItem(item);

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
        public RedirectToRouteResult DeleteInventoryItem(int InventoryItemId) //needs modelState validation
        {

            _itemServices.DeleteItem(InventoryItemId);

            return RedirectToAction("IndexInventory", "InventoryItem");
        }
        #endregion

        #region Search Requests 
        //Returns Inventory records that match search criteria
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {

            IList<InventoryItem> model = _itemServices.HandlesSearchRequest(searchRequest);

            return View(model);
        }
        #endregion
    }
}