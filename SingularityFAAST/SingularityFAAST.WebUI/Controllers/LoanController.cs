using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;


namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        public ActionResult IndexLoan(string sort)
        {
            var services = new LoanServices();
            var model = services.GetAllLoans();
            return View(model);

            //ViewBag.DateSortParm = sort == "Date" ? "date_desc" : "Date";
            //var endDate = from s in SingularityDB.Loans  //obviously can't do this with these layered projects
            //select s;
            //endDate = endDate.OrderBy(s => s.EnrollmentDate);

            //return View("ManageLoans");   

        }

        public ActionResult RenewLoan()
        {

            return View("RenewLn");
        }

        public ActionResult EditLn()
        {
            return View("EditLoan");
        }

        public ActionResult CheckIn()
        {
            return View("CheckIn");
        }

        public ActionResult CancelLn()
        {
            return View("CancelLoan");
        }

        public ActionResult RenewLnItem()
        {
            return View("RenewItems");
        }

        [HttpPost]
        public ViewResult AddLoan(Loan loan)
        {
            var services = new LoanServices();
            services.SaveLoan(loan);
            return View();
        }
    }
}