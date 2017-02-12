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

                var loans = from c in context.Clients
                            join l in context.LoanMasters
                            on c.ClientId equals l.ClientId
                            join ld in context.LoanDetails
                            on l.LoanMasterId equals ld.LoanMasterId

                select new LoansClientsInventoryDTO()
                {
                    LoanNumber = l.LoanNumber,
                    DateCreated = l.DateCreated,
                    ClientId = c.ClientId,
                    LastName = c.LastName,
                    FirstName = c.FirstName,
                    Notes = ld.Notes
                };

                return loans.ToList();
            }
        }


        // GET all Loans with client name
        public IList<LoansClientsInventoryDTO> GetLoansByClientLastName(string searchby)
        {
            IList<LoansClientsInventoryDTO> allLoans = GetAllLoans();  //Gets all the loans from the GetAllLoans() method

            IList<LoansClientsInventoryDTO> filteredLoans =
                allLoans.Where(client => string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredLoans;
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




        //The original get all loans with client name
        public IList<LoansClientsInventoryDTO> GetLoansName(string clientName)  //String //GetLoans(int loanItemId)  //this param is how you'd use a checkbox to pass the item to this method.  Param wouldn't be here for "see all loans".  Need to implement Adrian's search feature.
        {
            using (var context = new SingularityDBContext())    //Get primary loan info - number, Client names, Date Made.  Individual items will show once this is clicked on, on next page (items in loan) and that is another script.
            {
                var results =   from c in context.Clients
                                join l in context.LoanMasters
                                on c.ClientId equals l.ClientId
                                where c.LastName == clientName
                                select new LoansClientsInventoryDTO()
                                {
                                    LoanNumber = l.LoanNumber,
                                    DateCreated = l.DateCreated,
                                    ClientId = c.ClientId,
                                    LastName = c.LastName,
                                    FirstName = c.FirstName,
                                };

                return results.ToList();
                
                //or use lambda =>
                //context.Clients.Join(context.LoanMasters, c => c.ClientId, l => l.ClientId,
                //    (c, l) => new {FirstName = c.FirstName, LoanId = l.LoanMasterId});
            }
        }

    }
}