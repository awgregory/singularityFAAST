﻿using System;
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
            model = string.IsNullOrWhiteSpace(searchRequest.SearchBy) ? lm_services.GetAllLoans() : lm_services.GetLoansByClientLastName(searchRequest.SearchBy);
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

        //This loads the page with a box, from View Items page top button 
        public ActionResult RenewLn(LoansClientsInventoryDTO loan)
        {
            IList<LoansClientsInventoryDTO> allItems = lm_services.GetAllItems();
            IList<LoansClientsInventoryDTO> model = allItems.Where(x => int.Equals(x.LoanMasterId, loan.LoanMasterId)).ToList();

            return View(model);
        }


        //This is executed when click yes in page with the box
        public ActionResult RenewAllItems(LoansClientsInventoryDTO loan) //(string loan)
        {
            //passed string LoanNumber
            //process renewal here (is checkin of loan and then add new loan)

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

            return View(filteredLoans);
        }


        //This executes the loan editing when you click yes on Edit Loan page
        [HttpPost]
        public RedirectToRouteResult UpdateLoan(LoansClientsInventoryDTO loan)  //or use AddLoanInfo model
        {
            var services = new LoanMasterServices();
            //services.EditLoanMaster(loan);
            //services.EditLoanDetails(loan);

            //    //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }

        #endregion




        #region Check In

        //Whole Loan 
        //This is the View Page with text boxes & one param passed in. Doesn't do any checking-in
        public ActionResult CheckIn(LoansClientsInventoryDTO loan)
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

            //var services = new LoanMasterServices();
            lm_services.CheckLoan(loan);


            //    //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }



        //Single Item
        //1. Displays check-in page with single item
        public ActionResult CheckItem(LoansClientsInventoryDTO loan)
        {
            //Show single item on load
            IList<LoansClientsInventoryDTO> model = lm_services.ViewItemsById(loan.InventoryItemId);

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



        #region Cancel Loan or Single Item

        public ActionResult CancelLn(LoansClientsInventoryDTO loan)  //use inventoryItemId
        {
            //process delete here, return to Index
            return RedirectToAction("Index", "Loan");
        }


        public ActionResult CancelItem(LoansClientsInventoryDTO loan)  //use inventoryItemId
        {
            //process delete here, return to Index
            return RedirectToAction("Index", "Loan");
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

            //Returns to Loan Index page
            return RedirectToAction("Index", "Loan");
        }

        #endregion



        #region Search

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

