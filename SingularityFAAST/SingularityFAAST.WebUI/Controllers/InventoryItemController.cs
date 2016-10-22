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


        public ActionResult NewInventoryItem()
        {
            return View();
        }
    }
}