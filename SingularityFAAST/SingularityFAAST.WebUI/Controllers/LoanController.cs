using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingularityFAAST.WebUI.Models;

namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        public ActionResult Index(string sort)
        {
            ViewBag.DateSortParm = sort == "Date" ? "date_desc" : "Date";
            //var endDate = from s in SingularityDB.Loans  //obviously can't do this with these layered projects
                           //select s;
            //endDate = endDate.OrderBy(s => s.EnrollmentDate);

            return View("ManageLoans");  
        }
    }
}