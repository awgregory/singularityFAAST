using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.DataAccess.Contexts;
using System;
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

                select new LoansClientsInventoryDTO()
                {
                    LoanNumber = l.LoanNumber,
                    DateCreated = l.DateCreated,
                    ClientId = c.ClientID,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                };

                return loans.ToList();
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


        public IList<LoansClientsInventoryDTO> GetAllLoanItems(int loanNum)
        {
            using (var context = new SingularityDBContext())
            {
                //Get all loan items in a loan
                var loans = from ld in context.LoanDetails
                    join l in context.LoanMasters
                    on ld.LoanMasterId equals l.LoanMasterId
                    join i in context.InventoryItems
                    on ld.InventoryItemId equals i.InventoryItemId
                    where l.LoanNumber == loanNum

                    select new LoansClientsInventoryDTO()
                    {
                        LoanNumber = l.LoanNumber,
                        DateCreated = ld.LoanDate,
                        ItemName = i.ItemName,
                        Manufacturer = i.Manufacturer,


                    };

                return loans.ToList();
                IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();
                    //Gets all the loans from the GetAllLoans() method    //should be getallitems() where loan num = loan num

                IList<LoansClientsInventoryDTO> filteredLoans =
                    allLoans.Where(loan => int.Equals(loan.LoanNumber, loanNum)).ToList();

                return filteredLoans;
            }
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