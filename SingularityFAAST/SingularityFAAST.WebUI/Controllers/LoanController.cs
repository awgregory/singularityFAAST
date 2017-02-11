using System.Collections.Generic;
using System.Web.Mvc;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;


namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanMasterServices lm_services = new LoanMasterServices();

        //Original, using only LoanMaster

        // GET: All Loans
        //public ActionResult Index()  //(string sort) for search
        //{

        //    var services = new LoanMasterServices();
        //    IList<LoanMaster> model = lm_services.GetAllLoans();

        //    //IList<GetLoanInfo> model = lm_services.GetLoans();
        //    return View(model);

        //    //for search
        //    //ViewBag.DateSortParm = sort == "Date" ? "date_desc" : "Date";
        //    //var endDate = from s in SingularityDB.Loans  //obviously can't do this with these layered projects
        //    //select s;
        //    //endDate = endDate.OrderBy(s => s.EnrollmentDate);

        //    //return View("Index");
        //}



        //New
        //GET: Loans by Client Name
        public ActionResult Index()  //(string sort) for search
        {

            var services = new LoanMasterServices();
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            //IList<GetLoanInfo> model = lm_services.GetLoans();
            return View(model);

            //for search
            //ViewBag.DateSortParm = sort == "Date" ? "date_desc" : "Date";
            //var endDate = from s in SingularityDB.Loans  //obviously can't do this with these layered projects
            //select s;
            //endDate = endDate.OrderBy(s => s.EnrollmentDate);

            //return View("Index");
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
        public ViewResult AddLoan(LoanMaster loan)
        {
            var services = new LoanMasterServices();
            services.SaveLoan(loan);
            return View();
        }
    }
}