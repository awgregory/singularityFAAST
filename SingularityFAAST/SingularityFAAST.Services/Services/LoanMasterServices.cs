using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
//using RazorEngine;  //for the email template parsing
//using RazorEngine.Templating;   //for the email template parsing

namespace SingularityFAAST.Services.Services
{
    public class LoanMasterServices
    {
        //Original, using only Loan Master

        //public IList<LoanMaster> GetAllLoans()   
        //{
        //    using (var context = new SingularityDBContext())
        //    {
        //        var loans = context.LoanMasters;

        //        var loanList = loans.ToList();

        //        return loanList;
        //    }
        //}

        //public IList<LoansClientsInventoryDTO> GetAllofEverything()
        //{
        //    using (var context = new SingularityDBContext())
        //    {
        //        LoansClientsInventoryDTO orderView = new LoansClientsInventoryDTO();
        //        orderView.OrderData = (from i in context.InventoryItems select i).ToList();
        //        orderView.OrderDetailData = (from c in context.Clients select c).ToList();

        //        return orderView;
        //    }
        //}

//GetFromDB------------------------------------------------------------------------------------------------------------------------------------

            //Get All Loans in DB
        public IList<LoansClientsInventoryDTO> GetAllLoans()
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Loans in DB
                var loans = from c in context.Clients
                            join l in context.LoanMasters
                            on c.ClientID equals l.ClientId

                            //join ld in context.LoanDetails   //union
                            //on l.LoanMasterId equals ld.LoanMasterId  selects less and repeats because not all loans have loan details, and each detail then appears as a new loan

                select new LoansClientsInventoryDTO()
                {
                    LoanNumber = l.LoanNumber,
                    DateCreated = l.DateCreated,
                    ClientId = c.ClientID,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    IsActive = l.IsActive,
                    //LoanDate = ld.LoanDate
                };

                return loans.ToList();
            }
        }

        //Get All Loans in DB   //this method is a troubleshooting duplicate of the one above
        public IList<LoansClientsInventoryDTO> GetAllLoansByNum()
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Loans in DB
                var loans = from l in context.LoanMasters
                            join c in context.Clients
                            on l.ClientId equals c.ClientID

                            //join ld in context.LoanDetails   //union
                            //on l.LoanMasterId equals ld.LoanMasterId  selects less and repeats because not all loans have loan details, and each detail then appears as a new loan

                            select new LoansClientsInventoryDTO()
                            {
                                LoanNumber = l.LoanNumber,
                                DateCreated = l.DateCreated,
                                ClientId = c.ClientID,
                                LastName = c.LastName,
                                FirstName = c.FirstName,
                                IsActive = l.IsActive,
                            };

                return loans.ToList();
            }
        }

        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> GetAllItems()  //string loanNum, now filtered in the call instead
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Items in DB
                var items = from i in context.InventoryItems
                            join ld in context.LoanDetails
                            on i.InventoryItemId equals ld.InventoryItemId
                            join lm in context.LoanMasters
                            on ld.LoanMasterId equals lm.LoanMasterId
                            join c in context.Clients
                            on lm.ClientId equals c.ClientID
                            //where lm.LoanNumber.Equals(loanNum)  //put this filter in the call rather than here, so this can be reused

                select new LoansClientsInventoryDTO()
                {
                    LoanNumber = lm.LoanNumber,
                    LoanDate = ld.LoanDate,
                    InventoryItemId = i.InventoryItemId,
                    ItemName = i.ItemName,
                    Manufacturer = i.Manufacturer,
                    Description = i.Description,
                    Notes = ld.Notes,
                    HomePhone = c.HomePhone,
                    Email = c.Email,
                    Availability = i.Availability,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    IsActive = lm.IsActive
                };

                return items.ToList();
            }
        }


        public IList<LoansClientsInventoryDTO> GetAllClients()
        {
            using (var context = new SingularityDBContext())
            {
                //LoansClientsInventoryDTO dto = new LoansClientsInventoryDTO();
                //Client client = new Client();

                var clients = from c in context.Clients
                              join l in context.LoanMasters
                              on c.ClientID equals l.ClientId
                              join ld in context.LoanDetails
                              on l.LoanMasterId equals ld.LoanMasterId
                              join i in context.InventoryItems
                              on ld.InventoryItemId equals i.InventoryItemId

                select new LoansClientsInventoryDTO()
                {
                    HomePhone = c.HomePhone,
                    Email = c.Email,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    ClientId = c.ClientID,
                    LoanEligibility = c.LoanEligibility,

                    InventoryItemId = i.InventoryItemId,
                    ItemName = i.ItemName,
                    Manufacturer = i.Manufacturer,
                    Description = i.Description,
                };
                var clientList = clients.ToList();

                return clientList;

            }
        }
        

        // GET all Loans associated with client last name
        public IList<LoansClientsInventoryDTO> GetLoansByClientLastName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();  //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans = allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }

        //Search by Loan Number (also a string)
        public IList<LoansClientsInventoryDTO> GetLoanByLoanNumber(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoansByNum();  //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans = allLoans.Where(loan => string.Equals(loan.LoanNumber, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }
        
        //Get all Clients associated with Client last name
        public IList<LoansClientsInventoryDTO> GetClientsByName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allClients = GetAllClients();  //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans = allClients.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }
        
        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> ViewAllItems(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems();
            IList<LoansClientsInventoryDTO> filteredLoans = allItems.Where(loan => string.Equals(loan.LoanNumber, loanNumber)).ToList();

            return filteredLoans;
        }


        //This method not necessary!
        //Get all Inventory Items associated with LoanNumber
        //public IList<LoansClientsInventoryDTO> GetAllLoanItems(string loanNum)
        //{
        //    IList<LoansClientsInventoryDTO> allItems = GetAllItems(loanNum);  //Gets all the items from the GetAllItems() method - maybe that method should only return inventory items, not client?


        //    var selectedLoan = allItems.ToList();   

        //    //filtered in GetAllItems() instead
        //    //IList<LoansClientsInventoryDTO> selectedLoan =
        //    //    allItems.Where(loan => string.Equals(loan.LoanNumber, loanNum, StringComparison.OrdinalIgnoreCase)).ToList();

        //    return selectedLoan;

        //}


//CheckInItems--------------------------------------------------------------------------------------------------------------------------------------------
        

        //1. Updates the CheckIn DB fields -- Must update all three CheckIn s at once for whole loan
        public void CheckInLoanDetails(LoanDetail loan)
        {
            using (var context = new SingularityDBContext())
            { 
                //update LoanDetails each item ClientOutcome
                //var updateDetail = loan.LoanDetailIds.Select(lmId => new LoanDetail { LoanDetailId = loan.LoanDetailId, ClientOutcome = loan.ClientOutcome, Notes = loan.Notes});
                //update LoanDetails each item Notes

                context.LoanDetails.Attach(loan);

                var entry = context.Entry(loan);

                entry.State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        //2. Updates the CheckIn DB fields -- Must update all three CheckIn s at once for whole loan
        public void CheckInLoanMasters(LoanMaster loan)
        {
            using (var context = new SingularityDBContext())
            {
                //update LoanMasters IsActive to False
                loan.IsActive = false;

                context.LoanMasters.Attach(loan);

                var entry = context.Entry(loan);

                entry.State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        //3. Updates the CheckIn DB fields -- Must update all three CheckIn s at once for whole loan
        public void CheckInLoanInventory(InventoryItem loan)  //Edit InventoryItem
        {
            using (var context = new SingularityDBContext())
            {

                //for each item in InventoryItemId[], in same LoanNumber.  
                //foreach (var VARIABLE in COLLECTION)
                //{
                //    //put the below lines in here
                //}


                //update InventoryItems each item Availability to True
                //var updateInvItem = loan.InventoryItemIds.Select(lmId => new InventoryItem { Availability = loan.Availability, Damages = loan.Damages });
                //update InventoryItems each item Damages

                context.InventoryItems.Attach(loan);

                var entry = context.Entry(loan);

                entry.State = EntityState.Modified;

                context.SaveChanges();
            }
        }

//SaveAllItems---------------------------------------------------------------------------------------------------------------------------------------------

        //Renews all Items in a loan as a new loan
        public void SaveAllItemsAsNewLoan(LoansClientsInventoryDTO loan)
        {
            using (var context = new SingularityDBContext())
            {
                //Table1 - add: LoanMaster-> DateCreated, ClientId, IsActive
                loan.DateCreated = DateTime.Now;  

                var newMaster = loan.LoanMasterIds.Select(lmId => new LoanMaster { LoanMasterId = loan.LoanMasterId, ClientId = loan.ClientId, LoanNumber = loan.LoanNumber,  IsActive = loan.IsActive});   

                context.LoanMasters.AddRange(newMaster);

                context.SaveChanges();


                //Table2 - add: Details -> LoanNumber, LoanDate, InventoryItemId, Purpose, PurposeType
                loan.LoanDate = DateTime.Now;

                //for each item in InventoryItemId[], keep same LoanNumber.  
                //foreach (var VARIABLE in COLLECTION)
                //{
                //    //put the below line in here
                //}
                var newDetail = loan.LoanDetailIds.Select(lmId => new LoanDetail { LoanDetailId = loan.LoanDetailId, LoanMasterId = loan.LoanMasterId, InventoryItemId = loan.InventoryItemId, Purpose = loan.Purpose, PurposeType = loan.PurposeType});   

                context.LoanDetails.AddRange(newDetail);

                context.SaveChanges();

            }
        }


//EditItems-------------------------------------------------------------------------------------------------------------------------------------

        public void EditLoanDetails(LoanDetail loan)
        {
            using (var context = new SingularityDBContext())
            {
                context.LoanDetails.Attach(loan);

                var entry = context.Entry(loan);

                entry.State = EntityState.Modified;

                context.SaveChanges();

            }
        }

        public void EditLoanMaster(LoanMaster loan)
        {
            using (var context = new SingularityDBContext())
            {
                context.LoanMasters.Attach(loan);

                var entry = context.Entry(loan);

                entry.State = EntityState.Modified;

                context.SaveChanges();

            }
        }

        
        
//EmailNotify----------------------------------------------------------------------------------------------------------------------------------
        //Poll the DateCreated in LoanMaster every 24 hours. Trigger this email  if ((item.DateCreated.AddDays(28) <= DateTime.Now.AddDays(7) && item.DateCreated.AddDays(28) >= DateTime.Now) && (item.IsActive)) 
        //Add Email Notification
        public void NotifyEmail(string loanNumber)  //or LoanClientsInventoryDTO
        {
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/SingularityFAAST.WebUI/Scripts/EmailTemplate.html"));

            IList<LoansClientsInventoryDTO> model = ViewAllItems(loanNumber);

            //var le = new LoansClientsInventoryDTO()
            //{
            //    LoanNumber = model.LoanNumber,
            //    LastName = LastName,
            //    FirstName = FirstName,
            //    HomePhone = HomePhone,
            //    ItemName = ItemName,
            //    LoanDate = LoanDate.Add(28),
            //};

            //var body = RazorEngine.Parse(template, model);  // le?    bring in nuget razor engine?
            var body = "Hi, < br > Client<loan.FirstName>, < loan.LastName >, Phone<loan.PhoneNumber>, has a loan due within one week.Loan Number < loan.LoanNumber > has devices < devices > < br > Thanks, FAASTer";

                   var fromAddress = new MailAddress("kyoungbe@gmail.com", "FAASTer Inventory System");
            var toAddress = new MailAddress("kyoungbe@example.com", "FAAST Admin");
            const string fromPassword = "Ralph2888";
            const string subject = "Device Loan Coming Due";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var messageEmail = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })

                try
                {
                    smtp.Send(messageEmail);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                                ex.ToString());
                }

        }

    }
}