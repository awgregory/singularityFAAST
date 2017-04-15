using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
            model = model.OrderByDescending(loan => loan.DateCreated).ToList();
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
            model = string.IsNullOrWhiteSpace(searchRequest.SearchBy) ? lm_services.GetAllLoans() : lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
            return View(model);
        }
        #endregion



        #region View Items in Loan
        [HttpGet]
        public ActionResult ViewItems(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loanNumber);

            //testing email
            //lm_services.NotifyEmail(loanNumber);

            //Remove Item will also show this page:
            //IList<LoansClientsInventoryDTO> model = lm_services.removeItem(viewButton);   Not worked out yet
            return View(model);
        }

        //This is the page with the inventory items list in a loan
        //[HttpPost]
        //public ActionResult ViewItems(string loanNumber) //loanNumber
        //{
        //    IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loanNumber);

        //    //testing email
        //    //lm_services.NotifyEmail(loanNumber);

        //    //Remove Item will also show this page:
        //    //IList<LoansClientsInventoryDTO> model = lm_services.removeItem(viewButton);   Not worked out yet
        //    return View(model);
        //}

        #endregion




        #region Renew

        
        public ActionResult RenewLn(LoansClientsInventoryDTO loan)
        {
            IList<LoansClientsInventoryDTO> allItems = lm_services.GetAllItems();
            IList<LoansClientsInventoryDTO> model = allItems.Where(x => int.Equals(x.LoanMasterId, loan.LoanMasterId)).ToList();

            return View(model);
        }


        [HttpGet]
        public ActionResult CheckInRenewal(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loanNumber);  
                                     
            
            
            return View(model);
        } 


        [HttpPost]
        public ActionResult CheckInRenewal(LoansClientsInventoryDTO loan)
        {
            //
            //Get the list of inventory items

            //1. Close loan, save outcomes, check in items
            lm_services.RenewLoan(loan);

            //2. create new loan with new loan object so creates new loan number

            // The DTO needs to send all of the data needed here   
            //var submission = new LoansClientsInventoryDTO()
            //{
            //    ClientId = loan.ClientId,
            //    InventoryItemIds = loan.InventoryItemIds,
            //    IsActive = loan.IsActive,
            //    Purpose = loan.Purpose,
            //    PurposeType = loan.PurposeType
            //};
            
            lm_services.CreateRenewedLoan(loan);

            //3. use id to fill in loan with loan details

            //IList<LoansClientsInventoryDTO> model = lm_services.AddAllItemsAsNewLoan(loanNumber);
            //lm_services.SaveAllItemsAsNewLoan();

            return RedirectToAction("Index", "Loan");
        }


        //RenewLn.cshtm needs to pass the Id to CheckInRenewal First then collect up the input for the loan
        public ActionResult RenewAllItems(LoansClientsInventoryDTO loan) 
        {
     
            //IList<LoansClientsInventoryDTO> model = lm_services.AddAllItemsAsNewLoan(loanNumber);
            //lm_services.SaveAllItemsAsNewLoan();

            return RedirectToAction("Index", "Loan");
        }




        //This renews an individual item from View Items page
        public ActionResult RenewItem()  //(int id)
        {
            //process renewal here (is checkin of single item and then add new loan)
            //no further pages, routes back to Index

            return RedirectToAction("Index", "Loan");
        }

        #endregion




        #region Edit

        //This displays Edit Loan page
        public ActionResult EditLoan(LoansClientsInventoryDTO loan)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();

            IList<LoansClientsInventoryDTO> filteredLoans =
                model.Where(item => int.Equals(item.LoanMasterId, loan.LoanMasterId))
                    .ToList();

            //call jquery script, pass it json filtered object like above but json. Don't need above
            //var list = GetItemsListForEdit(loan.LoanMasterId);


            return View(filteredLoans);
        }


        //This executes the loan editing when you click submit on Edit Loan page
        [HttpPost]
        public ActionResult UpdateLoan(LoansClientsInventoryDTO loan)  
        {
            lm_services.EditLoan(loan);
            
            //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }

        #endregion




        #region Check In

        //Whole Loan 
        //This is the View Page with text boxes & one param passed in. Doesn't do any checking-in
        public ActionResult CheckIn(LoansClientsInventoryDTO loan)   // Only gets LoanNumber passed in
        {
            //View all Items in Loan
            //IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();  //get all for this id
            IList<LoansClientsInventoryDTO> model = lm_services.ViewAllItems(loan.LoanNumber);

            return View(model);
        }

        //Whole Loan
        //Executes the CheckIn process, routes to Services to update the DB, and then back to Index  //RedirectToRouteResult
        [HttpPost]
        public ActionResult CheckInLoan(LoansClientsInventoryDTO loan)
        {
            //Check in multiple items
            //Get input from text boxes for Damages, Notes and ClientOutcome

            //TODO: rewire as necessary
            var dto = new CheckInWholeLoanDTO()
            {
                ClientOutcome = loan.ClientOutcome,
                Damages = loan.Damages,
                LoanNotes = loan.LoanNotes,
                LoanNumber = loan.LoanNumber
            };

            lm_services.CheckInLoan_Nick(loan);


            //    //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }


        //Are either of these used?
        //Single Item
        //1. Displays check-in page with single item
        public ActionResult CheckItem(int inventoryItemId)
        {
            //Show single item on load
            IList<LoansClientsInventoryDTO> model = lm_services.ViewItemsById(inventoryItemId);

            return View(model);
        }

        //Single Item
        //2. Executes single item checkin, routes back to Index 
        public ActionResult CheckItemIn(LoansClientsInventoryDTO loan)
        {
            //Check in single item
            lm_services.CheckInLoanInventoryItem(loan);

            return RedirectToAction("Index", "Loan");
        }
        
        #endregion

        #region Delete Loan - Mark entirely deleted

        //route to "are you sure?" page
        public ActionResult CancelLoan(LoansClientsInventoryDTO loan)
        {
            IList<LoansClientsInventoryDTO> allItems = lm_services.GetAllItems();
            IList<LoansClientsInventoryDTO> model = allItems.Where(x => string.Equals(x.LoanNumber, loan.LoanNumber)).ToList();
            return View(model);
        }
        
        //Do the canceling
        public ActionResult CancelLn(string loanNumber)
        {
            lm_services.DeleteLoanByLoanNumber(loanNumber);

            return RedirectToAction("Index");
        }

        #endregion

        
        #region Delete Single Item - actually removes it from loan, no record left of its addition
        //[HttpPost]
        public ActionResult CancelItem(LoansClientsInventoryDTO loan)  
        {
            //process delete here, return to ViewItems
            var loanNum = (lm_services.RemoveSingleItemFromLoanByLoanNumber(loan.InventoryItemId)).ToString();
            //return RedirectToAction("ViewItems", "Loan", new {id = loanNum});
            return RedirectToAction("Index");
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


        //[HttpPost]
        //public async Task<ActionResult> AddLoan(LoansClientsInventoryDTO loanSubmission)  //(LoanSubmission loanSubmission)
        //{
        //    return null;
        //}



        //Called by AddLoan and EditLoan
        //Controls the Add Loan process, routes to Services to update the DB, and then back to Index  - does the actual adding
        [HttpPost]
        //public ActionResult AddTheLoan(LoansClientsInventoryDTO loanSubmission)  //(LoanSubmission loanSubmission)
        public ActionResult AddTheLoan(LoanSubmission loanSubmission)
        {
            var services = new LoanMasterServices();

            services.CreateLoan(loanSubmission);

            return RedirectToAction("Index", "Loan");
        }

        #endregion



        #region Search
        //the following two methods are used by ClientInventorySearch.js
        public JsonResult SearchFakeClients(string searchString)
        {
            IList<Client> fakeClients = lm_services.GetAllClientsAsInventoryList();

            //search through the fake clients list, checking eaching fake client if their first name contains the search string
            //var filteredClients = fakeClients.Where(fakeClient => fakeClient.LastName.ToLower().Contains(searchString) && fakeClient.Active);
            var filteredClients = fakeClients.Where(c => c.LastName.ToLower().Contains(searchString.ToLower()) && c.Active).ToList();
            //var filteredClients = fakeClients.Where(fakeClient => fakeClient.LastName.ToLower().Contains(searchString)).ToList();

            //method demands return type of Json, whose first parameter is DATA, just shove the c# result into this
            //and javascript on the front end will be happy
            return Json(filteredClients, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SearchInventory(string searchString)
        {
            IList<InventoryItem> inventoryItems = lm_services.GetAllItemsAsInventoryList();
            //var filteredItems = inventoryItems.Where(ii => ii.ItemName.ToLower().Contains(searchString) && ii.Availability);       //string.Equals(ii.ItemName, searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            var filteredItems = inventoryItems.Where(thing => thing.ItemName.ToLower().Contains(searchString.ToLower()) && thing.Availability).ToList();
            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetItemsListForEdit(int searchInt)
        {
            IList<LoansClientsInventoryDTO> model = lm_services.GetAllItems();
            var filteredLoans = model.Where(item => int.Equals(item.LoanMasterId, searchInt)) .ToList();
            return Json(filteredLoans, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteIteminEdit(string searchstring)
        {
            lm_services.RemoveSingleItemFromLoanByItemEdit(searchstring);
            return RedirectToAction("EditLoan", "Loan");
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

