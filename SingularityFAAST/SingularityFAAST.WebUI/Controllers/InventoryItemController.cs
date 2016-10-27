using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SingularityFAAST.WebUI.Controllers
{
    public class InventoryItemController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }
        //returns list of Inventory Items
        //public ActionResult ListInventoryItems()
        //{
        //    return View(db.InventoryItem.ToList());
        //}

        public ActionResult NewInventoryItem()
        {
            return View();
        }

        public RedirectToRouteResult MethodAddNewItem()
        {
            return null; //would eventually collect form info and add item to db and return user to inventory home page
        } 
    }
}