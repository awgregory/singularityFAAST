﻿using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.CodeDom;
using System.Collections.Generic;
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


        //New, with DTO
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



        //Get all Inventory Items associated with LoanNumber
        public IList<LoansClientsInventoryDTO> GetAllLoanItems(string loanNum)
        {
            IList<LoansClientsInventoryDTO> allItems = GetAllItems(loanNum);  //Gets all the items from the GetAllItems() method - maybe that method should only return inventory items, not client?

            
            var selectedLoan = allItems.ToList();   
            
            //filtered in GetAllItems() instead
            //IList<LoansClientsInventoryDTO> selectedLoan =
            //    allItems.Where(loan => string.Equals(loan.LoanNumber, loanNum, StringComparison.OrdinalIgnoreCase)).ToList();

            return selectedLoan;

        }


        public IList<LoansClientsInventoryDTO> CheckInLoan(string loanNum)
        {

        }


        public void SaveLoan(LoanMaster loan)
        {
            using (var context = new SingularityDBContext())
            {
                loan.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db

                context.LoanMasters.Add(loan);

                var rowsAffected = context.SaveChanges();
            }
        }

    }
}