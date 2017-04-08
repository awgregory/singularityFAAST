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
using System.Security.Policy;
using System.Web;
using SingularityFAAST.Services.Services;
using System.Data.SqlClient;
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


        #region Get All From DB - 3 basic methods for Loans, Clients, Inventory

        //Get All Loans in DB
        public IList<LoansClientsInventoryDTO> GetAllLoans()
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Loans in DB
                var loans = from c in context.Clients  //LINQ Query Syntax form, end with Select instead of starting with it
                            join l in context.LoanMasters
                            on c.ClientID equals l.ClientId
                            where l.IsDeleted == false
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


        //Gets all Inventory Items
        public IList<LoansClientsInventoryDTO> GetAllItems()  
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

                            where lm.IsDeleted == false

                            select new LoansClientsInventoryDTO()
                            {
                                LoanMasterId = lm.LoanMasterId,
                                LoanNumber = lm.LoanNumber,
                                ClientId = c.ClientID,
                                LoanDate = ld.LoanDate,
                                InventoryItemId = i.InventoryItemId,
                                ItemName = i.ItemName,
                                Manufacturer = i.Manufacturer,
                                Description = i.Description,
                                LoanNotes = ld.Notes,
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

                var clients = from c in context.Clients
                              join l in context.LoanMasters
                              on c.ClientID equals l.ClientId
                              join ld in context.LoanDetails
                              on l.LoanMasterId equals ld.LoanMasterId
                              join i in context.InventoryItems
                              on ld.InventoryItemId equals i.InventoryItemId

                              where l.IsDeleted == false

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
        #endregion


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

                            where l.IsDeleted == false

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

        public void DeleteLoanByLoanNumber(string loanNumber)
        {
            using (var context = new SingularityDBContext())
            {
                var loanToDelete = context.LoanMasters.FirstOrDefault(
                    loanMaster =>
                        string.Equals(loanMaster.LoanNumber, loanNumber.ToString())
                    );

                if (loanToDelete != null)
                    loanToDelete.IsDeleted = true;

                context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);
            }
        }

        public void RemoveSingleItemFromLoanByLoanNumber(string loanNumber)  //not correct yet
        {
            using (var context = new SingularityDBContext())
            {
                var itemToRemove = context.LoanDetails.FirstOrDefault(
                    inventoryItem => string.Equals(inventoryItem.InventoryItemId, loanNumber.ToString())  //join loandetails
                    );

                if (itemToRemove != null)
                    //itemToRemove.Availability = true;

                    context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);
            }
        }

        public Client GetClientDetails()  //int id
        {
            using (var context = new SingularityDBContext())
            {
                var client = context.Clients;
                var clients = client.FirstOrDefault();

                return clients;
            }
        }


        //Get most recent loan number
        public string LoanIncrement()
        {
            using (var context = new SingularityDBContext())
            {
                var loan = (from l in context.LoanMasters
                            orderby l.LoanMasterId descending
                            select l.LoanNumber).Take(1);
                var addNumber = string.Join("", loan.Where(char.IsDigit));
                int lastNum = Int32.Parse(addNumber);
                lastNum = lastNum + 1;
                return lastNum.ToString();
            }
        }


        // GET all Loans associated with client last name
        public IList<LoansClientsInventoryDTO> GetLoansByClientLastName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();  //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans = allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }


        #region AddLoan
        public void CreateLoan(LoanSubmission loanSubmission)  //InventoryItemIds not passing
        {
            //have client id 
            using (var context = new SingularityDBContext())
            {
                var loanNumIncrement = LoanIncrement();

                var newLoan = new LoanMaster
                {
                    ClientId = loanSubmission.ClientId,
                    DateCreated = DateTime.Now, //can go into the constructor of LoanMaster  -- haven't done this yet
                                                //IsActive = loanSubmission.IsActive //can probably be defaulted
                    LoanNumber = loanNumIncrement,  //we can create a utility class that auto increments this - see above
                    IsActive = true,
                    IsDeleted = false
                };

                context.LoanMasters.Add(newLoan);
                context.SaveChanges(); //this line actually writes the record to the db

                //after writing the record the newLoan object will have a populated Id that matches the db
                //in this way we can add LoanDetails that reference the correct LoanMaster

                //have item Ids

                //we can map over the itemIds enumerable and map a new list of LoanDetails -- functional!

                //LINQ query syntax


                //Create new LoanDetail for each item in InventoryItemIds list
                IEnumerable<LoanDetail> query = from itemId in loanSubmission.InventoryItemIds
                                                select new LoanDetail
                                                {
                                                    InventoryItemId = itemId,
                                                    LoanMasterId = newLoan.LoanMasterId,
                                                    Purpose = loanSubmission.Purpose,
                                                    PurposeType = loanSubmission.PurposeType
                                                };

                //LINQ method syntax - would be same outcome as above query
                //IEnumerable<LoanDetail> methodQuery =
                //    loanSubmission.InventoryItemIds.Select(id => new LoanDetail
                //    {
                //        InventoryItemId = id,
                //        LoanMasterId = newLoan.LoanMasterId
                //    });

                //both are viable, sometimes query syntax feels more natural on joins and such
                //while method syntax can output some nice oneliners.
                //the point is that we use each item in a collection to create new LoanDetail
                //entities, mapping the properties we want over to the newly created object.

                //regardless, these queries have only returned something respects IEnumerable contract
                //we need concrete objects

                List<LoanDetail> loanDetailsList = query.ToList();

                //now we can add a range of loan details to the loan details table
                context.LoanDetails.AddRange(loanDetailsList);
                context.SaveChanges();


                ////Update Inventory Items' Availability
                var itemIds = GetInventoryItemIdsByLoanNumber(newLoan.LoanNumber);
                MarkInventoryItemsAsNotAvailable(context, itemIds);  //or does this need to be like context.LoanDetails.AddRange(loanDetailsList);

            }
        }

        #endregion


        //Search by Loan Number (also a string)
        public IList<LoansClientsInventoryDTO> GetLoanByLoanNumber(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoansByNum();  //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans = allLoans.Where(loan => string.Equals(loan.LoanNumber, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }



        //Get all INVENTORYITEMS
        public IList<InventoryItem> GetAllItemsAsInventoryList()
        {
            using (var context = new SingularityDBContext())
            {
                var items = context.InventoryItems;

                var itemList = items.ToList();

                return itemList;
            }
        }



        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> ViewAllItems(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems();
            IList<LoansClientsInventoryDTO> filteredLoans = allItems.Where(loan => string.Equals(loan.LoanNumber, loanNumber)).ToList();

            return filteredLoans;
        }


        public IList<InventoryItem> ViewItemsByName(string itemName)
        {
            IList<InventoryItem> allItems = GetAllItemsAsInventoryList();
            IList<InventoryItem> items = allItems.Where(inventory => string.Equals(inventory.ItemName, itemName)).ToList();

            return items;
        }


        public IList<LoansClientsInventoryDTO> ViewItemsById(int itemId)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems();
            IList<LoansClientsInventoryDTO> items = allItems.Where(inventory => int.Equals(inventory.InventoryItemId, itemId)).ToList();

            return items;
        }


        #region CheckInSingleItem
        //Updates the Inventory CheckIn DB fields 
        public void CheckInLoanInventoryItem(LoansClientsInventoryDTO loan)  //Edit InventoryItem
        {
            using (var context = new SingularityDBContext())
            {
                var loanNum = loan.LoanNumber;

                //var loanNum = GetLoanNumberByInventoryItemId(inventoryId);
                var itemIds = GetInventoryItemIdsByLoanNumber(loanNum);  

                //itemIds is IEnumerable<int>

                if (itemIds.Count() > 1)
                {
                    var inventoryItemid = loan.InventoryItemId; 

                    var itemId = context.InventoryItems.FirstOrDefault(
                        ii => int.Equals(ii.InventoryItemId, inventoryItemid));

                    if (itemId != null)
                    {
                        itemId.Availability = true;
                        itemId.Damages = loan.Damages;
                        //do not worry about item damages for now
                    }
                    context.SaveChanges();
                }
                else
                {
                    //CheckInLoan_Nick(loan);
                    var loanNumber = loan.LoanNumber;

                    var query = context.LoanMasters.FirstOrDefault(
                    lm => string.Equals(lm.LoanNumber, loanNumber));

                    if (query != null)
                    {
                        query.IsActive = false;  //is not changing to false.  LoanMaster.cs has [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

                        //query.ClientOutcome = loan.ClientOutcome;
                        //query.LoanNotes = loan.LoanNotes;
                        //do not worry about item damages for now
                    }

                    context.SaveChanges();

                    var itemIds2 = GetInventoryItemIdsByLoanNumber(loanNumber);

                    MarkInventoryItemsAsAvailable(context, itemIds2);
                }
            }
        }
        #endregion



        #region CheckInLoan

        public void CheckInLoan_Nick(LoansClientsInventoryDTO checkInDTO) // copied into RenewLoan below
        {
            var loanNumber = checkInDTO.LoanNumber;

            using (var context = new SingularityDBContext())
            {
                var loan = context.LoanMasters.FirstOrDefault(
                    lm => string.Equals(lm.LoanNumber, loanNumber));

                if (loan != null)
                {
                    loan.IsActive = false;

                    loan.ClientOutcome = checkInDTO.ClientOutcome;
                    loan.LoanNotes = checkInDTO.LoanNotes;
                    //do not worry about item damages for now
                }

                context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);


            }
        }

        #endregion


        //Renews all Items in a loan as a new loan
        public void RenewLoan(LoansClientsInventoryDTO renewedDTO) 
        {
            var loanNumber = renewedDTO.LoanNumber;

            using (var context = new SingularityDBContext())
            {
                var loan = context.LoanMasters.FirstOrDefault(
                    lm => string.Equals(lm.LoanNumber, loanNumber));

                if (loan != null)  // Always check for Null
                {
                    loan.IsActive = false;

                    loan.ClientOutcome = renewedDTO.ClientOutcome;
                    loan.LoanNotes = renewedDTO.LoanNotes;
                    //do not worry about item damages for now
                }

                context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);


            }
        }
            //    using (var context = new SingularityDBContext())
            //    {

            //        var query2 = from item in context.LoanDetails
            //                     where item.InventoryItemId == loan.InventoryItemId
            //                     select item;

            //        //Details - may have multiple deets, so for each loan detail, update
            //        //var query1 = (from itemId in loan.InventoryItemIds
            //        //                where itemId == loan.InventoryItemId
            //        //              //select itemId).ToList();
            //        //              select new LoanDetail
            //        //              {
            //        //                  InventoryItemId = itemId,
            //        //                  ClientOutcome = loan.ClientOutcome,
            //        //                  Notes = loan.Notes
            //        //              });

            //        List<LoanDetail> loanDetailsList = query2.ToList();
            //        foreach (var iItem in loanDetailsList)
            //        {
            //            iItem.ClientOutcome = loan.ClientOutcome;
            //            iItem.Notes = loan.Notes;
            //        }
            //        context.SaveChanges();


            //        //old, not specific but didn't trigger error
            //        //IEnumerable <LoanDetail> itemIds = new List<LoanDetail>();
            //        //foreach (var iItem in itemIds) 
            //        //{
            //        //    iItem.ClientOutcome = loan.ClientOutcome;
            //        //    iItem.Notes = loan.Notes;
            //        //}
            //        //context.SaveChanges();


            //        ////Inventory - for each item (one item per detail) update these fields:  
            //        //IEnumerable<InventoryItem> query2 = new List<InventoryItem>();
            //        //query2 = query2.Select(itemId => itemId.InventoryItemId = loan.InventoryItemIds).ToList();

            //        var inventoryList = (from item in context.InventoryItems where item.InventoryItemId == loan.InventoryItemId select item).ToList();
            //        foreach (var iItem in inventoryList)
            //        {
            //            iItem.Availability = true;
            //            iItem.Damages = loan.Damages;
            //        }
            //        context.SaveChanges();


            //        //Master
            //        //LoanMaster loanM = context.LoanMasters.SingleOrDefault(x => Equals(x.LoanMasterId, loan.LoanMasterId));
            //        var loanM = (from item in context.LoanMasters where item.LoanMasterId == loan.LoanMasterId select item).ToList();
            //        foreach (var items in loanM)
            //        {
            //            items.IsActive = false;
            //        }
            //        context.SaveChanges();

            //    }
            //}



            #region EditItems

            //public void EditLoanDetails(LoanDetail loan)
            //{
            //    using (var context = new SingularityDBContext())
            //    {
            //        context.LoanDetails.Attach(loan);

            //        var entry = context.Entry(loan);

            //        entry.State = EntityState.Modified;

            //        context.SaveChanges();

            //    }
            //}

            //public void EditLoanMaster(LoanMaster loan)
            //{
            //    using (var context = new SingularityDBContext())
            //    {
            //        context.LoanMasters.Attach(loan);

            //        var entry = context.Entry(loan);

            //        entry.State = EntityState.Modified;

            //        context.SaveChanges();

            //    }
            //}
            #endregion


            #region SaveAllItems

            //Renews all Items in a loan as a new loan
            public void SaveAllItemsAsNewLoan(LoansClientsInventoryDTO loan)  //(LoanSubmission loan)
        {
            using (var context = new SingularityDBContext())
            {
                //Table1 - add: LoanMaster-> DateCreated, ClientId, IsActive
                loan.DateCreated = DateTime.Now;
                loan.IsActive = true;

                //var newMaster = loan.LoanMasterIds.Select(lmId => new LoanMaster { LoanMasterId = loan.LoanMasterId, ClientId = loan.ClientId, LoanNumber = loan.LoanNumber,  IsActive = loan.IsActive});   
                var newMaster = loan.LoanMasterIds.Select(lmId => new LoanMaster { ClientId = loan.ClientId, IsActive = loan.IsActive });

                context.LoanMasters.AddRange(newMaster);

                context.SaveChanges();


                //Table2 - add: Details -> LoanNumber, LoanDate, InventoryItemId, Purpose, PurposeType
                loan.LoanDate = DateTime.Now;

                //for each item in InventoryItemId[], keep same LoanNumber.  
                //foreach (var VARIABLE in COLLECTION)
                //{
                //    //put the below line in here
                //}
                var newDetail = loan.LoanDetailIds.Select(lmId => new LoanDetail { LoanDetailId = loan.LoanDetailId, LoanMasterId = loan.LoanMasterId, InventoryItemId = loan.InventoryItemId, Purpose = loan.Purpose, PurposeType = loan.PurposeType });

                context.LoanDetails.AddRange(newDetail);

                context.SaveChanges();

            }
        }
        #endregion


        #region EmailNotification
        //Poll the DateCreated in LoanMaster every 24 hours. Trigger this email  if ((item.DateCreated.AddDays(28) <= DateTime.Now.AddDays(7) && item.DateCreated.AddDays(28) >= DateTime.Now) && (item.IsActive)) 
        //Add Email Notification
        //use Windows service running on vm Windows rt 
        public void NotifyEmail(string loanNumber)  //or LoanClientsInventoryDTO
        {
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/SingularityFAAST.WebUI/Views/Loan/EmailTemplate.html"));

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

            var fromAddress = new MailAddress("faasterEmailNotification@gmail.com", "FAASTer Inventory System");
            var toAddress = new MailAddress("kyoungbe@gmail.com", "FAAST Admin");
            const string fromPassword = "faastemail";
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
        #endregion

        private IEnumerable<int> GetInventoryItemIdsByLoanNumber(string loanNumber)
        {
            using (var context = new SingularityDBContext())
            {
                var storedProcedureName = "dbo.GetInventoryItemsByLoanNumber @loanNumber";
                var pLoanNumber = new SqlParameter("@loanNumber", loanNumber);

                return context.Database.SqlQuery<int>(
                    storedProcedureName, pLoanNumber).ToList();
            }
        }

        private IEnumerable<string> GetLoanNumberByInventoryItemId(int inventoryitemid)
        {
            using (var context = new SingularityDBContext())
            {
                var storedProcedureName = "dbo.GetLoanNumberByInventoryItemId @inventoryItemId";
                var pInventoryItemId = new SqlParameter("@inventoryItemId", inventoryitemid);

                return context.Database.SqlQuery<string>(
                    storedProcedureName, pInventoryItemId).ToList();
            }
        }

        private void MarkInventoryItemsAsAvailable(SingularityDBContext context,
            IEnumerable<int> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                var item = context.InventoryItems.FirstOrDefault(
                    i => i.InventoryItemId == itemId);

                if (item != null)
                    item.Availability = true;

                context.SaveChanges();
            }
        }


        private void MarkInventoryItemsAsNotAvailable(SingularityDBContext context,
           IEnumerable<int> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                var item = context.InventoryItems.FirstOrDefault(
                    i => i.InventoryItemId == itemId);

                if (item != null)
                    item.Availability = false;

                context.SaveChanges();
            }
        }
    }
}