using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web.Mvc;
using SingularityFAAST.Core.DataTransferObjects;
//using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;


namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanMasterServices lm_services = new LoanMasterServices();
        private readonly ClientServices _clientServices = new ClientServices();

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
        public ActionResult Index()  
        {
            var services = new LoanMasterServices();
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            return View(model);

            //return View("Index");
        }



        //GET: Loans by Client Name  
        [HttpPost]
        public ActionResult Index(SearchByString searchRequest)  //If routing to LoanMasterServices, mine is SearchByString (see below)
        {
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();
                return View(model);
            }

            //if (string contains numbers? regex? theirs begin with rl-3333 etc.)
            //{
            //    IList<LoansClientsInventoryDTO> model = lm_services.GetLoanByLoanNumber(searchRequest.SearchBy);
            //    return View(model);
            //}

            else
            {
                //IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchByName);
                IList<LoansClientsInventoryDTO> model = lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
                return View(model);
            }
        }



        //should be a DTO?  Corresponds with form item on Index.cshtml
        public class SearchByString
        {
            public string SearchBy { get; set; }
        }

        public class PassALoanNumber
        {
            public string LoanNum { get; set; }
        }


        
        public ActionResult RenewLoan(PassALoanNumber loanNumber)
        {

            //IList<LoansClientsInventoryDTO> model = lm_services. Do i need method here or is this just passing value from page to page   (loanNumber.LoanNum);
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

        //public ActionResult RenewLnItem()
        //{
        //    return View("RenewItems");
        //}


        [HttpGet]
        public ActionResult RenewItems(PassALoanNumber loanNumber)  //(string sort) for search
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoanItems(loanNumber.LoanNum);
            return View(model);

        }



        public ActionResult AddLoan()
        {
            //var services = new LoanMasterServices();
            //services.SaveLoan(loan);
            return View();
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