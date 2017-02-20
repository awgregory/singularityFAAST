using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ClientServices _clientServices = new ClientServices();  //to use Adrian's? 


        //GET: All Loans
        [HttpGet]
        public ActionResult Index()  
        {
           IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            return View(model);

        }



        //GET: Loans by Client Name  
        [HttpPost]
        public ActionResult Index(SearchByString searchRequest)  //If routing to LoanMasterServices, mine is SearchByString (see below)
        {
            //if (searchRequest.byNum == "Search")
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))

            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();
                return View(model);
            }
            if (searchRequest.SearchBy.Any(char.IsDigit))
            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetLoanByLoanNumber(searchRequest.SearchBy);
                return View(model);
            }
            else
            {
                //IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchByName);  //to use Adrian's?
                IList<LoansClientsInventoryDTO> model = lm_services.GetLoansByClientLastName(searchRequest.SearchBy); //or do this and do split logic in LMServices not here
                return View(model);
            }

        }


        ////GET: Loans by Loan Number  
        //[HttpPost]
        //public ActionResult Index(SearchByString searchRequest, string byNum)
        //{
        //    //if (lnNum.Any(c => char.IsDigit(c)))                                          

        //    else
        //    {
        //        IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public ActionResult loanItemsActions(SearchRequest searchRequest)
        //{
           
        //}


//This is the page with the inventory items list in a loan
[HttpPost]
        public ActionResult RenewItems(string loanNumber)  
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoanItems(loanNumber);
            return View(model);
        }





        //This is the page with a box 
        public ActionResult RenewLoan(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoanItems(loanNumber);
            //process renewal here

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


        //should use SearchRequests files made by Adrian ?  Used by form item on Index.cshtml
        public class SearchByString
        {
            public string SearchBy { get; set; }
            public string SearchByNum { get; set; }
        }

        public class PassALoanNumber
        {
            public string LoanNum { get; set; }
        }

    }
}
