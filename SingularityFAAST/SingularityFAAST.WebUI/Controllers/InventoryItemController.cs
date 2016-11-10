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


        public ActionResult Index()
        {
            return View();
        }
        

        //new inventory page
        public ActionResult NewInventoryItem()
        {
            return View();
        }

        //View All inventory page
        public ActionResult ViewAllInventory()
        {
            var inventoryServices = new InventoryItemServices();
            var model = inventoryServices.GetAllInventory();
            return View(model);
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
    }
}