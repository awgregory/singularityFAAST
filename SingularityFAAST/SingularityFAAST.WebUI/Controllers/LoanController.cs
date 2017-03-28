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
    [Authorize]
    public class LoanController : Controller
    {
        private readonly LoanMasterServices lm_services = new LoanMasterServices();
        private readonly ClientServices _clientServices = new ClientServices(); 
        private readonly InventoryItemServices ii_services = new InventoryItemServices();


        #region Index /All Loans Page
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
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllLoans();

            int n;
            if (int.TryParse(searchRequest.SearchBy, out n))
            {
                model = lm_services.GetLoanByLoanNumber(searchRequest.SearchBy);
                return View(model);
            }

            //else
            model = string.IsNullOrWhiteSpace(searchRequest.SearchBy)? lm_services.GetAllLoans(): lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
            return View(model);
        }
#endregion



        #region ViewItem

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

        #endregion




        #region Renew

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

        #endregion

 


        #region Edit

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

        #endregion




        #region Check In

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

        #endregion



        #region Cancel Loan

        public ActionResult CancelLn()
        {
            return View("CancelLoan");
        }

        #endregion



        #region Add Loan

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


        [HttpPost]
        public async Task<ActionResult> AddLoan(LoansClientsInventoryDTO loanSubmission)  //(LoanSubmission loanSubmission) 
        {
            return null;
        }


        //Called by AddLoan and EditLoan
        //Controls the Add Loan process, routes to Services to update the DB, and then back to Index  - does the actual adding
        [HttpPost]
        public ActionResult AddTheLoan(LoansClientsInventoryDTO loanSubmission)  //(LoanSubmission loanSubmission)
        {
            var services = new LoanMasterServices();
            services.SaveAllItemsAsNewLoan(loanSubmission);

            //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }

        //[HttpPost]
        //public RedirectToRouteResult AddTheLoan(LoansClientsInventoryDTO loan)
        //{
        //        var services = new LoanMasterServices();
        //        services.SaveAllItemsAsNewLoan(loan);

        //    //    //Returns to Loan Index page
        //    return RedirectToAction("Index", "Loan");
        //}

        #endregion



        #region Search

        //Displays Inventory search results on page
        //[HttpPost]
        //public ActionResult SearchInventory(SearchByString searchRequest, SearchByString byName)  //or do AddLoan with the two parms
        //{
        //    //no search term entered
        //    if (string.IsNullOrWhiteSpace(searchRequest.SearchBy))
        //    {
        //        var list1 = _clientServices.GetAllClients();
        //        var list2 = ii_services.GetAllInventory();
        //        var model = new AddLoanInfo(list1, list2);
        //        return View(model);
        //    }
        //    else
        //    {
        //        //get Inventory item by name
        //        //Client list1 = lm_services.GetClientsByLName(searchRequest.SearchBy);
        //        var list1 = _clientServices.GetClientByLastName(searchRequest.SearchBy);
        //        var list2 = lm_services.ViewItemsByName(searchRequest.byName);
        //        var model = new AddLoanInfo(list1, list2);
        //        //return View(model);
        //        return RedirectToRoute("AddTheLoan", new { clientid = searchRequest.SearchBy });  //pass to AddLoan above 
        //    }

        //}


        public JsonResult SearchFakeClients(string searchString)
        {
            IList<Client> fakeClients = _clientServices.GetAllClients();

            //search through the fake clients list, checking eaching fake client if their first name contains the search string
            //var filteredClients = fakeClients.Where((fakeClient) => fakeClient.LastName.Contains(searchString));
            var filteredClients = fakeClients.Where((fakeClient) => string.Equals(fakeClient.LastName, searchString, StringComparison.OrdinalIgnoreCase)).ToList();

            //method demands return type of Json, whose first parameter is DATA, just shove the c# result into this
            //and javascript on the front end will be happy
            return Json(filteredClients, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchInventory(string searchString)
        {
            IList<InventoryItem> inventoryItems = lm_services.GetAllItemsAsInventoryList();
            var filteredItems = inventoryItems.Where((ii) => ii.ItemName.Contains(searchString));       //string.Equals(ii.ItemName, searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
      
        
        //Search classes
        public class SearchByString
        {
            public string SearchBy { get; set; }
            public string byName { get; set; }

        }
        #endregion


    }
}

