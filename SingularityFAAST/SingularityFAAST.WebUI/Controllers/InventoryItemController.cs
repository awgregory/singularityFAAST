using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingularityFAAST.WebUI.Controllers
{
    public class InventoryItemController : Controller
    {
        //private InventoryDBContext db = new InventoryDBContext();


        public ActionResult Index()
        {
            return View();
        }
        ////returns list of Inventory Items
        //public ActionResult ListInventoryItems()
        //{
        //    var allInventory = new 
        //    return View(db.InventoryItem.ToList());
        //}

        //new inventory page
        public ActionResult NewInventoryItem()
        {
            return View();
        }

        //View All inventory page
        public ActionResult ViewAllInventory()
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
    }
}