using System.Collections.Generic;
using SingularityFAAST.Core.SearchRequests;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Services.Services;
using System;

namespace SingularityFAAST.WebUI.Controllers
{
    [Authorize]
    public class InventoryItemController : Controller
    {
        private readonly InventoryItemServices itemServices = new InventoryItemServices();

       //This is the inventory home page
        public ActionResult IndexInventory()
        {
            var model = itemServices.GetAllInventory();
            return View(model);
        }



        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        //NewInventoryItem methods
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        
        //New Item Creation
        //pushes next Inventory Item number into form
        [HttpGet]
        public ActionResult NewInventoryItem()
        {
            int itemCount = itemServices.ReturnNextInventoryNumber();
            IList<InventoryItem> model = itemServices.GetAllInventory();
            
            return View(model);//returns two types
            //return View(model);
        }

        //Collects data from New Inventory Item form
        [HttpPost]
        public ActionResult NewInventoryItem(InventoryItem item)
        {
            var services = itemServices;

            services.SaveNewItem(item);

            return RedirectToAction("IndexInventory", "InventoryItem");
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^



        
        //``````````````````````````````````````````
        //UpdateInventoryItem methods
        //``````````````````````````````````````````

        //update inventory item view
        public ActionResult UpdateInventoryItem()
        {
            return View();
        }

        //post method for updating item after user makes changes
        //  -->Redirects user to InventoryIndex
        [HttpPost]
        public RedirectToRouteResult UpdateInventoryItem(InventoryItem item)
        {
            var services = new InventoryItemServices();

            services.EditExistingItem(item);

            return RedirectToAction("IndexInventory", "InventoryItem");
        }
        //``````````````````````````````````````````




        //returns view for All Available Inventory
        public ActionResult ViewAllAvailableInv()
        {
            var services = new InventoryItemServices();
            var model = services.ViewAvailableInv();
            return View(model);
        }

        //returns view for All Inventory on Loan
        public ActionResult ViewAllOnLoanInv()
        {
            var services = new InventoryItemServices();
            var model = services.ViewInvOnLoan();
            return View(model);
        }

        ////  Returns Inventory records that match search criteria
        [HttpPost]
        public ActionResult Index(SearchRequest searchRequest)
        {

            IList<InventoryItem> model = itemServices.HandlesSearchRequest(searchRequest);

            return View(model);
        }
    }
}