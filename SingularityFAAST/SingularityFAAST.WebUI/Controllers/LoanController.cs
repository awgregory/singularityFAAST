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
        public ActionResult Index(SearchByString searchRequest)
            //If routing to LoanMasterServices, mine is SearchByString (see below)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            int n;
            if(int.TryParse(searchRequest.SearchBy, out n))
            {
                 model = lm_services.GetLoanByLoanNumber(searchRequest.SearchBy);
                return View(model);
            }

            model = string.IsNullOrWhiteSpace(searchRequest.SearchBy) ? lm_services.GetAllLoans() : lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
            return View(model);
        }


//--------------------------------------------------------------------------------------------------------------------

        //This is the page with the inventory items list in a loan
        [HttpPost]
        public ActionResult ViewItems(string loanNumber)    //loanNumber
        {
            IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loanNumber);

            //testing email
            //lm_services.NotifyEmail(loanNumber);

            //Remove Item will also show this page:
            //IList<LoansClientsInventoryDTO> model = lm_services.removeItem(viewButton);   Not worked out yet
            return View(model);
        }
        
        


        //This is the page with a box 
        public ActionResult RenewLn(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();
            //process renewal here

            return View("RenewLn");
        }



        public ActionResult RenewItem(string loan)
        {
            //List Item

            return View("RenewItem");
        }




        public ActionResult RenewAllItems(LoansClientsInventoryDTO loan)
        {
            //IList<LoansClientsInventoryDTO> model = lm_services.AddAllItemsAsNewLoan(loanNumber);
            var services = lm_services;

            services.SaveAllItemsAsNewLoan(loan);

            return View("Index");
        }

//Edit----------------------------------------------------------------------------------------------------------------------------

        public ActionResult EditLn(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();

            IList<LoansClientsInventoryDTO> filteredLoans = model.Where(loan => string.Equals(loan.LoanNumber, loanNumber, StringComparison.OrdinalIgnoreCase)).ToList();

            return View("EditLoan");
        }

        //[HttpPost]
        //public RedirectToRouteResult EditLoan(LoansClientsInventoryDTO loan)
        //{
        //    var services = new LoanMasterServices();
        //    services.EditLoanMaster(loan);

        //    var services2 = new LoanMasterServices();
        //    services2.EditLoanDetails(loan);

        //    //    //Returns to Loan Index page
        //    return RedirectToAction("Index", "Loan");
        //}


//CheckIn------------------------------------------------------------------------------------------------------------------------------

        //View Page with text boxes & one param passed in
        public ActionResult CheckIn(string inventoryItemId)
        {
            //View Items in Loan
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();

            //View text boxes for Damages, Notes and ClientOutcome

            return View(model);
        }



        

        public ActionResult CheckItem(string inventoryItemId)
        {   
            //View Item
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();

            //View text boxes for Damages, Notes and ClientOutcome
           
            return View(model);
        }




        //Called by CheckIn and CheckItem
        //Controls the CheckIn process, routes to Services to update the DB, and then back to Index
        [HttpPost]
        public RedirectToRouteResult CheckInLoan(LoansClientsInventoryDTO loan)
        {
            //    var services = new LoanMasterServices();
            //    services.SaveLoan(loan);

            //    //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }






        public ActionResult CancelLn()
        {
            return View("CancelLoan");
        }



//AddLoan-----------------------------------------------------------------------------------------------------------------------------------

        //Displays initial AddLoan Page with empty boxes
        public ViewResult AddLoan()  //Loan case does not use loanNum but it might be used by Client use case
        {
            //IList<LoansClientsInventoryDTO> model = lm_services.GetAllClients();

            IList<LoansClientsInventoryDTO> model2 = lm_services.GetAllItems();

            
            //IList<LoansClientsInventoryDTO> filteredLoans =
            //model2.Where(loan => string.Equals(loan.LoanNumber, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            IList<LoansClientsInventoryDTO> list = new List<LoansClientsInventoryDTO>();
            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(new LoansClientsInventoryDTO { ClientCategoryId = i, Type = "Type" + i });  //Don't show the number, only Type name
            //}

            //model.ClientCategorySelectList = new SelectList(lm_services.ClientCategory, "ClientCategory", "Name");

            //return View(list);  //how to get all these lists to the view?

            return View(model2);  //model2

            //return View();
        }


        //Displays Client search results on page 
        [HttpPost]
        public ActionResult AddLoan(SearchByString searchRequest)  //If routing to LoanMasterServices, mine is SearchByString (see below)
        {
            
            //if (searchRequest.byNum == "Search")
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
            {
                IList<LoansClientsInventoryDTO> model = lm_services.GetAllClients();
                return View(model);
            }
            else
            {
                //IList<Client> model = _clientServices.GetClientsByName(searchRequest.SearchByName);  //to use Adrian's?
                IList<LoansClientsInventoryDTO> model = lm_services.GetClientsByName(searchRequest.SearchBy); //or do this and do split logic in LMServices not here
                return View(model);
            }

        }




        //Called by AddLoan and EditLoan
        //Controls the Add Loan process, routes to Services to update the DB, and then back to Index  - does the actual adding 

        //[HttpPost]
        //public RedirectToRouteResult AddLoan(LoansClientsInventoryDTO loan)
        //{
        ////    var services = new LoanMasterServices();
        ////    services.SaveLoan(loan);

        ////    //Returns to Loan Index page
        //    return RedirectToAction("Index", "Loan");
        //}


//SearchBy------------------------------------------------------------------------------------------------------------------------------------------------


        //Search classes
        //should use SearchRequests files made by Adrian ?  Used by form items
        public class SearchByString
        {
            public string SearchBy { get; set; }
            public string byNum { get; set; }
            //public string SearchByNum { get; set; }
        }

        public class PassALoanNumber
        {
            public string LoanNum { get; set; }
        }

    }
}
