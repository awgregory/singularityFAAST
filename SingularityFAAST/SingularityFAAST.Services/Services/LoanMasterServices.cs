using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SingularityFAAST.Services.Services
{
    public class LoanMasterServices
    {
        public IList<LoanMaster> GetAllLoans()   //was LoanClientItem
        {
            using (var context = new SingularityDBContext())
            {
                var loans = context.LoanMasters;

                var loanList = loans.ToList();

                return loanList;
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


        //public IList<Client> GetLoansName(string clientName)  //GetLoans(int loanItemId)  //this param is how you'd use a checkbox to pass the item to this method.  Param wouldn't be here for "see all loans".  Need to implement Adrian's search feature.
        //{
        //    using (var context = new SingularityDBContext())    //Get primary loan info - number, Client names, Date Made.  Individual items will show once this is clicked on, on next page (items in loan) and that is another script.
        //    {
        //        //var results =

        //            //from c in context.Clients
        //            //join l in context.LoanMasters
        //            //on c.ClientId equals l.ClientId
        //            //where c.LastName == clientName
        //            //select c;


        //        //var resultList = results.ToList();
        //        //return resultList;


        //        //or use lambda =>
        //        //context.Clients.Join(context.LoanMasters, c => c.ClientId, l => l.ClientId,
        //        //    (c, l) => new {FirstName = c.FirstName, LoanId = l.LoanMasterId});
        //    }
        //}

        //public IList<LoanMaster> GetByID(int Id)
        //{
        //    //
        //}

        //public IList<LoanClientItem> GetLoanByName(string searchby)
        //{
        //    IList<LoanClientItem> allClients = GetAllLoans();

        //    IList<LoanClientItem> filteredClients = allClients.Where(client =>
        //        string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

        //    return filteredClients;
        //}

    }
}