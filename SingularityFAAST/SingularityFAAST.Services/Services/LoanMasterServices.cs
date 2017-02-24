using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;

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


        //Get All Loans in DB
        public IList<LoansClientsInventoryDTO> GetAllLoans()
        {
            using (var context = new SingularityDBContext())
            {
                //Get all Loans in DB
                var loans = from c in context.Clients
                            join l in context.LoanMasters
                            on c.ClientID equals l.ClientId

                            //join ld in context.LoanDetails
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

        

        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> GetAllItems(string loanNum)
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
                            where lm.LoanNumber.Equals(loanNum)

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



        // GET all Loans associated with client name
        public IList<LoansClientsInventoryDTO> GetLoansByClientLastName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();  //Gets all the loans from the GetAllLoans() method

            IList<LoansClientsInventoryDTO> filteredLoans =
                allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
        }

        //Search by Loan Number (also a string)
        public IList<LoansClientsInventoryDTO> GetLoanByLoanNumber(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();  //Gets all the loans from the GetAllLoans() method

            IList<LoansClientsInventoryDTO> filteredLoans =
                allLoans.Where(loan => string.Equals(loan.LoanNumber, searchby)).ToList();

            return filteredLoans;
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
                        //{
                        //dto.FirstName = client.FirstName;
                        //dto.LastName = client.LastName;
                        //dto.ClientId = client.ClientID;
                        //}
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



        public IList<LoansClientsInventoryDTO> GetClientsByName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllClients();  //Gets all the loans from the GetAllLoans() method

            IList<LoansClientsInventoryDTO> filteredLoans =
                allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

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




        //Updates the CheckIn DB fields
        public void CheckInLoan(string loanNum)
        {
            //update LoanMasters IsActive to False
            //update InventoryItems each item Availability to True
            //update InventoryItems each item Damages
            //update LoanDetails each item ClientOutcome
            //update LoanDetails each item Notes
        }


        //Updates the AddLoan and EditLoan fields
        public void SaveLoan(LoansClientsInventoryDTO loan)
        {
        //    using (var context = new SingularityDBContext())
        //    {
        //        loan.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

        //        context.LoanMasters.Add(loan);

        //        var rowsAffected = context.SaveChanges();
        //    }
        }

    }
}