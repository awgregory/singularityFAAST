using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        

        //new inventory view
        public ActionResult NewInventoryItem()
        {
            return View();
        }

        //update inventory item view
        public ActionResult UpdateInventoryItem()
        {
            return View();
        }

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

        public RedirectToRouteResult MethodAddNewItem()
        {
            return null; //would eventually collect form info and add item to db and return user to inventory home page
        }

        public RedirectToRouteResult MethodUpdateItem()
        {
            return null; //would eventually collect form info and add item to db and return user to inventory home page
        }
    }
}