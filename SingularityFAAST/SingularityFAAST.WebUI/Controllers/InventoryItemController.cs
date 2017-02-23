using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Services.Services;

namespace SingularityFAAST.WebUI.Controllers
{
    public class InventoryItemController : Controller
    {
       //This is the inventory home page
        public ActionResult IndexInventory()
        {
            var services = new InventoryItemServices();
            var model = services.GetAllInventory();
            return View(model);
        }


        //add inventory view
        public ViewResult NewInventoryItem()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult NewInventoryItem(InventoryItem item)
        {
            var services = new InventoryItemServices();

            services.SaveNewItem(item);

            return RedirectToAction("IndexInventory", "InventoryItem");
        }


        //update inventory item view
        public ActionResult UpdateInventoryItem()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult UpdateInventoryItem(InventoryItem item)
        {
            var services = new InventoryItemServices();

            services.EditExistingItem(item);

            return RedirectToAction("IndexInventory", "InventoryItem");
        }

        //don't remember why I had this, but I'll keep it just in case
        //public RedirectToRouteResult UpdateInventoryItem(InventoryItem item)

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
    }
}