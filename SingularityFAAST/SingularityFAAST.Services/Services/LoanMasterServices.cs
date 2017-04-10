using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Data.SqlClient;
//using RazorEngine;  //for the email template parsing
//using RazorEngine.Templating;   //for the email template parsing

namespace SingularityFAAST.Services.Services
{
    public class LoanMasterServices
    {
        #region BUGS
        //     _______
        //    |       |
        // ___|       |___
        //    |  O O  |      ______
        //    |   >   |     | Bugs |
        //    |__ 0 __|     | X X  |
        // ______| |______  |______|
        //|  _         _  |    ||
        //| | |       | | |    ||
        //| | |       | | |___ ||
        //| | |       | |_____|_}
        //|_| |       | 
        //{_} |_______|
        //    |       |
        //    |   ||  |
        //    |   ||  |
        //    |   ||  |
        //    |   ||  |
        //    |   ||  |
        //   [____||____]
        //needs fixin - medium priority
        #endregion


        #region Get All From DB - 3 basic methods for Loans, Clients, Inventory

        //Get All Loans in DB
        public IList<LoansClientsInventoryDTO> GetAllLoans()
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Loans in DB
                var loans = from c in context.Clients
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
                                CellPhone = c.CellPhone,
                                Email = c.Email
                                //LoanDate = ld.LoanDate
                            };

                return loans.ToList();
            }
        }


        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> GetAllItems() //string loanNum, now filtered in the call instead
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
                                LoanDate = ld.LoanDate,
                                InventoryItemId = i.InventoryItemId,
                                ItemName = i.ItemName,
                                Manufacturer = i.Manufacturer,
                                Description = i.Description,
                                LoanNotes = lm.LoanNotes,
                                CellPhone = c.CellPhone,
                                Email = c.Email,
                                Availability = i.Availability,
                                LastName = c.LastName,
                                FirstName = c.FirstName,
                                IsActive = lm.IsActive,
                                ClientId = c.ClientID,
                                ClientOutcome = lm.ClientOutcome,
                                Damages = i.Damages
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
                                  CellPhone = c.CellPhone,
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
                {
                    loanToDelete.IsDeleted = true;
                    loanToDelete.IsActive = false;
                }
                context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);
            }
        }


        public int RemoveSingleItemFromLoanByLoanNumber(int invItem)
        {
            int loanIdpassed = 0;
            using (var context = new SingularityDBContext())
            {
                var loanNumber = GetLoanNumberByInventoryItemId(invItem);
                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber.FirstOrDefault());
                var loanId = (from ld in context.LoanDetails
                              join lm in context.LoanMasters
                              on ld.LoanMasterId equals lm.LoanMasterId
                              where lm.IsActive == true && (ld.InventoryItemId == invItem)
                              select ld).FirstOrDefault();  //where (ld.InventoryItemId == invItem) && 

                //if there is more than one item in the loan, check in
                if (itemIds.Count() > 1)
                {
                    var item = context.InventoryItems.FirstOrDefault(
                        i => i.InventoryItemId == invItem);

                    if (item != null)
                    {
                        item.Availability = true;
                    }
                    context.SaveChanges();

                    //remove Loan Detail with item, from Loan
                    context.LoanDetails.Remove(loanId);
                    context.SaveChanges();

                    loanIdpassed = loanId.LoanMasterId;

                }
                else   //if there's only one item in loan, and it's about to be deleted
                {
                    //CheckInLoan_Nick(loan); - no because this is a delete when a mistake has been made. Item may never have left the building.

                    //remove the loan detail with that item from the loan
                    var loanId2 = (from ld in context.LoanDetails
                                   join lm in context.LoanMasters
                                   on ld.LoanMasterId equals lm.LoanMasterId
                                   where lm.IsActive == true && (ld.InventoryItemId == invItem)
                                   select ld).FirstOrDefault();

                    context.LoanDetails.Remove(loanId2);
                    context.SaveChanges();

                    //update Loan information in LoanMaster
                    var query = (from lm in context.LoanMasters
                                 where lm.LoanMasterId == loanId.LoanMasterId
                                 select lm).FirstOrDefault();

                    if (query != null)
                    {
                        query.IsActive = false;
                        query.IsDeleted = true;
                    }
                    context.SaveChanges();

                    //Update inventory item availability
                    var itemIds2 = GetInventoryItemIdsByLoanNumber(query.LoanNumber);
                    MarkInventoryItemsAsAvailable(context, itemIds2);

                    //pass value back for page load
                    loanIdpassed = loanId2.LoanMasterId;
                }
                return loanIdpassed;
            }
        }


        public void RemoveSingleItemFromLoanByItemEdit(string loanNum)
        {
            using (var context = new SingularityDBContext())
            {
                var itemIds = GetInventoryItemIdsByLoanNumber(loanNum);

                if (itemIds.Count() > 1)
                {
                    //get inv item to available
                    if (itemIds != null)
                    {
                        MarkInventoryItemsAsAvailable(context, itemIds);
                    }
                    var loanId = (from ld in context.LoanDetails
                                  join lm in context.LoanMasters
                                  on ld.LoanMasterId equals lm.LoanMasterId
                                  where lm.LoanNumber == loanNum
                                  select ld).FirstOrDefault();


                    //remove the loan detail itself
                    context.LoanDetails.Remove(loanId);
                    context.SaveChanges();
                }
                else
                {
                    var loanNumber = loanNum;

                    var query = context.LoanMasters.FirstOrDefault(
                        lm => string.Equals(lm.LoanNumber, loanNumber));

                    if (query != null)
                    {
                        query.IsActive = false;
                    }

                    context.SaveChanges();

                    var itemIds2 = GetInventoryItemIdsByLoanNumber(loanNumber);

                    MarkInventoryItemsAsAvailable(context, itemIds2);
                }
            }
        }


        public Client GetClientDetails() //int id
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
                            orderby l.DateCreated descending
                            select l.LoanNumber).Take(1).Single();
                var addNumber = string.Join("", loan.Where(char.IsDigit));
                int lastNum = Int32.Parse(addNumber);
                lastNum = lastNum + 1;
                return lastNum.ToString();
            }
        }


        // GET all Loans associated with client last name
        public IList<LoansClientsInventoryDTO> GetLoansByClientLastName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans(); //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans =
                //allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase))
                //    .ToList();
            allLoans.Where(thing => thing.LastName.ToLower().Contains(searchby.ToLower())).ToList();
            return filteredLoans;
        }


        #region AddLoan

        public void CreateLoan(LoanSubmission loanSubmission)
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
                    LoanNumber = loanNumIncrement, //we can create a utility class that auto increments this - see above
                    IsActive = true
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
                MarkInventoryItemsAsNotAvailable(context, itemIds);
                //or does this need to be like context.LoanDetails.AddRange(loanDetailsList);

            }
        }

        #endregion


        //Search by Loan Number (also a string)
        public IList<LoansClientsInventoryDTO> GetLoanByLoanNumber(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoansByNum();
            //Gets all the loans from the GetAllLoans() method
            IList<LoansClientsInventoryDTO> filteredLoans =
                //allLoans.Where(loan => string.Equals(loan.LoanNumber, searchby, StringComparison.OrdinalIgnoreCase))
                //    .ToList();
            allLoans.Where(thing => thing.LoanNumber.ToLower().Contains(searchby.ToLower())).ToList();
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

        public IList<Client> GetAllClientsAsInventoryList()
        {
            using (var context = new SingularityDBContext())
            {
                var items = context.Clients;

                var itemList = items.ToList();

                return itemList;
            }
        }



        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> ViewAllItems(string loanNumber)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems();
            IList<LoansClientsInventoryDTO> filteredLoans =
                allItems.Where(loan => string.Equals(loan.LoanNumber, loanNumber)).ToList();

            return filteredLoans;
        }


        public IList<InventoryItem> ViewItemsByName(string itemName)
        {
            IList<InventoryItem> allItems = GetAllItemsAsInventoryList();
            IList<InventoryItem> items =
                allItems.Where(inventory => string.Equals(inventory.ItemName, itemName)).ToList();

            return items;
        }


        public IList<LoansClientsInventoryDTO> ViewItemsById(int itemId)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems();
            IList<LoansClientsInventoryDTO> items =
                allItems.Where(inventory => int.Equals(inventory.InventoryItemId, itemId)).ToList();

            return items;
        }


        #region CheckInSingleItem

        //Updates the Inventory CheckIn DB fields 
        public void CheckInLoanInventoryItem(LoansClientsInventoryDTO loan) 
        {
            using (var context = new SingularityDBContext())
            {
                var loanNum = loan.LoanNumber;

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNum);

                //itemIds is IEnumerable<int>
                //Update Item
                if (itemIds.Count() > 1)
                {
                    var itemId = (from ii in context.InventoryItems
                        where ii.InventoryItemId == loan.InventoryItemId
                        select ii).FirstOrDefault();
                    
                    if (itemId != null)
                    {
                        itemId.Availability = true;
                        //itemId.Damages = loan.Damages;
                        //do not worry about item damages for now
                    }
                    context.SaveChanges();
                }
                else
                {
                    //CheckInLoan
                    var loanNumber = loan.LoanNumber;

                    var query = context.LoanMasters.FirstOrDefault(
                        lm => string.Equals(lm.LoanNumber, loanNumber));

                    if (query != null)
                    {
                        query.IsActive = false;
                    }

                    context.SaveChanges();

                    var itemIds2 = GetInventoryItemIdsByLoanNumber(loanNumber);

                    MarkInventoryItemsAsAvailable(context, itemIds2);
                }
            }
        }

        #endregion



        #region CheckInLoan

        public void CheckInLoan_Nick(LoansClientsInventoryDTO checkInDTO)
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
                    //loan.Damages = checkInDTO.Damages;
                }

                context.SaveChanges();

                var itemIds = GetInventoryItemIdsByLoanNumber(loanNumber);

                MarkInventoryItemsAsAvailable(context, itemIds);


            }
        }

        #endregion


        

        #region Edit Loan

        public void EditLoan(LoansClientsInventoryDTO loanSubmission)
        {
            //have client id 
            using (var context = new SingularityDBContext())
            {
                IEnumerable<LoanDetail> query = from itemId in loanSubmission.InventoryItemIds
                                                select new LoanDetail
                                                {
                                                    InventoryItemId = itemId,
                                                    LoanMasterId = loanSubmission.LoanMasterId,
                                                    Purpose = loanSubmission.Purpose,
                                                    PurposeType = loanSubmission.PurposeType
                                                };
                List<LoanDetail> loanDetailsList = query.ToList();

                ////Update New Inventory Items' Availability
                foreach (var itemId in query)
                {
                    var item = (from ii in context.InventoryItems
                                  where ii.InventoryItemId == itemId.InventoryItemId
                                  select ii).FirstOrDefault();

                    if (item != null)
                        item.Availability = false;

                    context.SaveChanges();
                }
                //now we can add a range of loan details to the loan details table
                context.LoanDetails.AddRange(loanDetailsList);
                context.SaveChanges();                              

            }
        }

        #endregion


        #region Unused - SaveAllItems

        //Renews all Items in a loan as a new loan
        //public void SaveAllItemsAsNewLoan(LoansClientsInventoryDTO loan)  //(LoanSubmission loan)
        //{
        //    using (var context = new SingularityDBContext())
        //    {
        //        //Table1 - add: LoanMaster-> DateCreated, ClientId, IsActive
        //        loan.DateCreated = DateTime.Now;
        //        loan.IsActive = true;

        //        //var newMaster = loan.LoanMasterIds.Select(lmId => new LoanMaster { LoanMasterId = loan.LoanMasterId, ClientId = loan.ClientId, LoanNumber = loan.LoanNumber,  IsActive = loan.IsActive});   
        //        var newMaster = loan.LoanMasterIds.Select(lmId => new LoanMaster { ClientId = loan.ClientId, IsActive = loan.IsActive });

        //        context.LoanMasters.AddRange(newMaster);

        //        context.SaveChanges();


        //        //Table2 - add: Details -> LoanNumber, LoanDate, InventoryItemId, Purpose, PurposeType
        //        loan.LoanDate = DateTime.Now;

        //        //for each item in InventoryItemId[], keep same LoanNumber.  
        //        //foreach (var VARIABLE in COLLECTION)
        //        //{
        //        //    //put the below line in here
        //        //}
        //        var newDetail = loan.LoanDetailIds.Select(lmId => new LoanDetail { LoanDetailId = loan.LoanDetailId, LoanMasterId = loan.LoanMasterId, InventoryItemId = loan.InventoryItemId, Purpose = loan.Purpose, PurposeType = loan.PurposeType });

        //        context.LoanDetails.AddRange(newDetail);

        //        context.SaveChanges();

        //    }
        //}

        #endregion


        #region EmailNotification

        //Poll the DateCreated in LoanMaster every 24 hours. Trigger this email  if ((item.DateCreated.AddDays(28) <= DateTime.Now.AddDays(7) && item.DateCreated.AddDays(28) >= DateTime.Now) && (item.IsActive)) 
        //Add Email Notification
        //use Windows service running on vm Windows rt 
        public void NotifyEmail(string loanNumber) //or LoanClientsInventoryDTO
        {
            var template =
                File.ReadAllText(
                    HttpContext.Current.Server.MapPath("~/SingularityFAAST.WebUI/Views/Loan/EmailTemplate.html"));

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
            var body =
                "Hi, < br > Client<loan.FirstName>, < loan.LastName >, Phone<loan.PhoneNumber>, has a loan due within one week.Loan Number < loan.LoanNumber > has devices < devices > < br > Thanks, FAASTer";

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

        private void MarkInventoryItemsAsAvailable(SingularityDBContext context, IEnumerable<int> itemIds)
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