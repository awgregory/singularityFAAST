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
       private InventoryItemServices db = new InventoryItemServices();

        //This is the inventory home page
        public ActionResult IndexInventory()
        {
            return View();
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

        public ActionResult ViewAllAvailableInv()
        {
            return View();
        }

        public ActionResult ViewAllOnLoanInv()
        {
            return View();
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