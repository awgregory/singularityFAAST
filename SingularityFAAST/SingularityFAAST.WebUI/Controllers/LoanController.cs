using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using SingularityFAAST.Core.DataTransferObjects;
//using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Services.Services;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;
using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.WebUI.Models;


namespace SingularityFAAST.WebUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly LoanMasterServices lm_services = new LoanMasterServices();
        private readonly ClientServices _clientServices = new ClientServices(); //to use Adrian's? 
        private readonly InventoryItemServices ii_services = new InventoryItemServices();


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
            if (int.TryParse(searchRequest.SearchBy, out n))
            {
                model = lm_services.GetLoanByLoanNumber(searchRequest.SearchBy);
                return View(model);
            }

            model = string.IsNullOrWhiteSpace(searchRequest.SearchBy)? lm_services.GetAllLoans(): lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
            return View(model);
        }


//--------------------------------------------------------------------------------------------------------------------

        //This is the page with the inventory items list in a loan
        [HttpPost]
        public ActionResult ViewItems(string loanNumber) //loanNumber
        {
            IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loanNumber);

            //testing email
            //lm_services.NotifyEmail(loanNumber);

            //Remove Item will also show this page:
            //IList<LoansClientsInventoryDTO> model = lm_services.removeItem(viewButton);   Not worked out yet
            return View(model);
        }

//RenewMethods - loan and details------------------------------------------------------------------------------------------------------------        


        //This is the page with a box 
        public ActionResult RenewLn(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();
            //process renewal here

            return View("RenewLn");
        }


        //This renews an individual item from View Items page
        public ActionResult RenewItem(string loan)
        {
            //List Item

            return View("RenewItem");
        }



        //This is executed when click yes in page with a box
        public ActionResult RenewAllItems(LoansClientsInventoryDTO loan)
        {
            //IList<LoansClientsInventoryDTO> model = lm_services.AddAllItemsAsNewLoan(loanNumber);

            lm_services.SaveAllItemsAsNewLoan(loan);

            return View("Index");
        }

//Edit----------------------------------------------------------------------------------------------------------------------------

        //This displays Edit Loan page
        public ActionResult EditLn(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();

            IList<LoansClientsInventoryDTO> filteredLoans =
                model.Where(loan => string.Equals(loan.LoanNumber, loanNumber, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            return View("EditLoan");
        }

        //This executes the loan editing when you click yes on Edit Loan page
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

        //This is just the View Page with text boxes & one param passed in. Doesn't do any checking-in
        public ActionResult CheckIn(string inventoryItemId)
        {
            //View all Items in Loan
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();  //get all for this id
            
            return View(model);
        }




        //From View Items page, this result returns same page but without check-in button for this item 
        public ActionResult CheckItem(string inventoryItemId)
        {
            //Check in single item
            //Get input from text boxes for Damages, Notes and ClientOutcome

            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();
            //Services method here to change this item to inactive

            return View(model);
        }




        //Called by CheckIn and CheckItem
        //Controls the CheckIn process, routes to Services to update the DB, and then back to Index
        [HttpPost]
        public RedirectToRouteResult CheckInLoan(LoansClientsInventoryDTO loan)
        {
            //Check in multiple items
            //Get input from text boxes for Damages, Notes and ClientOutcome

            //Services method here to change all items to inactive:
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

        //Displays initial AddLoan Page with empty input boxes
        [HttpGet]
        public ViewResult AddLoan()   //int id  //Loan case does not use loanNum but it might be used by Client use case
        {
            //var list1 = lm_services.GetClientDetails(); //GetAllClients();  //takes id
            var list1 = _clientServices.GetAllClients();
            var list2 = ii_services.GetAllInventory();
            var model = new AddLoanInfo(list1, list2);
            
            return View(model);
        }


        //Displays Client search results on page with all loans 
        //[HttpPost]
        ////public ActionResult SearchClient(SearchByString searchRequest)  //for separating both lists into method rather than back to view
        //public ActionResult AddLoan(SearchByString searchRequest)
        //{
        //    //no search term entered
        //    if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
        //    {
        //        //var list1 = lm_services.GetClientDetails();
        //        var list1 = _clientServices.GetAllClients();
        //        var list2 = ii_services.GetAllInventory();
        //        var model = new AddLoanInfo(list1, list2);
        //        return View(model); 
        //    }
        //    else
        //    {   
        //        //Get Client by last name
        //        //Client list1 = lm_services.GetClientsByLName(searchRequest.SearchBy);
        //        var list1 = _clientServices.GetClientByLastName(searchRequest.SearchBy);
        //        var list2 = ii_services.GetAllInventory();
        //        var model = new AddLoanInfo(list1, list2);
        //        return View(model); 
        //        //return RedirectToRoute("AddLoan", new { clientid = searchRequest.SearchBy});  //pass to AddLoan above 
        //    }

        //}



        [HttpPost]
        public async Task<ActionResult> AddLoan(LoanSubmission loanSubmission)
        {
            return null;
        }



        //Displays Inventory search results on page
        [HttpPost]
        public ActionResult SearchInventory(SearchByString searchRequest, SearchByString byName)  //or do AddLoan with the two parms
        {
            //no search term entered
            if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
            {
                var list1 = _clientServices.GetAllClients();
                var list2 = ii_services.GetAllInventory();
                var model = new AddLoanInfo(list1, list2);
                return View(model);
            }
            else
            {
                //get Inventory item by name
                //Client list1 = lm_services.GetClientsByLName(searchRequest.SearchBy);
                var list1 = _clientServices.GetClientByLastName(searchRequest.SearchBy);
                var list2 = lm_services.ViewItemsByName(searchRequest.byName);
                var model = new AddLoanInfo(list1, list2);
                //return View(model);
                return RedirectToRoute("AddTheLoan", new { clientid = searchRequest.SearchBy });  //pass to AddLoan above 
            }

        }
        

        #region Nick Code
        public JsonResult SearchFakeClients(string searchString)
        {
            var fakeClients = new List<Client>
            {
                new Client{ClientID = 1, FirstName = "Ferglehorn" },
                new Client{ClientID = 2, FirstName = "Ergtoof" },
                new Client{ClientID = 3, FirstName = "Osoltoof" },
                new Client{ClientID = 4, FirstName = "Atoofol" },
                new Client{ClientID = 5, FirstName = "Retoofum" },
                new Client{ClientID = 6, FirstName = "Toofensag" },
                new Client{ClientID = 7, FirstName = "Hornbreaker" },
                new Client{ClientID = 8, FirstName = "Ethornisk" },
            };

            //search through the fake clients list, checking eaching fake client if their first name contains the search string
            var filteredClients = fakeClients.Where((fakeClient) => fakeClient.FirstName.Contains(searchString));

            //method demands return type of Json, whose first parameter is DATA, just shove the c# result into this
            //and javascript on the front end will be happy
            return Json(filteredClients, JsonRequestBehavior.AllowGet);
        }
        #endregion 


        //Called by AddLoan and EditLoan
        //Controls the Add Loan process, routes to Services to update the DB, and then back to Index  - does the actual adding 

        //[HttpPost]
        //public RedirectToRouteResult AddTheLoan(LoansClientsInventoryDTO loan)
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
            public string byName { get; set; }

        }
    }
}

