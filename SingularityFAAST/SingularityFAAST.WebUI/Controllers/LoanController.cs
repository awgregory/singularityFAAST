using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SingularityFAAST.Core.DataTransferObjects;
//using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;


namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanMasterServices lm_services = new LoanMasterServices();
        //private readonly ClientServices _clientServices = new ClientServices();

        //Original, using only LoanMaster

        // GET: All Loans
        //public ActionResult Index()  //(string sort) for search
        //{

        //    var services = new LoanMasterServices();
        //    IList<LoanMaster> model = lm_services.GetAllLoans();

        //    //IList<GetLoanInfo> model = lm_services.GetLoans();
        //    return View(model);

        //    //return View("Index");
        //}



        //New
        //GET: All Loans
        [HttpGet]
        public ActionResult Index()  //(string sort) for search
        {
            var services = new LoanMasterServices();
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            return View(model);

            //return View("Index");
        }



        //GET: Loans by Client Name  
        [HttpPost]
        public ActionResult Index(SearchByCLastName searchRequest)  
        {
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();
                return View(model);
            }

            else
            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
                return View(model);
            }
        }


        //should be a DTO?  Corresponds with form item on Index.cshtml
        public class SearchByCLastName
        {
            public string SearchBy { get; set; }
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