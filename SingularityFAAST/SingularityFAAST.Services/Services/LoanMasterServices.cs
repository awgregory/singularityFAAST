using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;


namespace SingularityFAAST.Services.Services
{
    public class LoanMasterServices
    {
        public IList<LoanMaster> GetAllLoans()
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

        public IList<string> GetLoans(string lastName)  //GetLoans(int loanItemId)  //this param is how you'd use a checkbox to pass the item to this method.  Param wouldn't be here for "see all loans".  Need to implement Adrian's search feature.
        {
            using (var context = new SingularityDBContext())  //Get primary loan info - number, Client names, Date Made.  Individual items will show once this is clicked on, on next page (items in loan) and that is another script.
            {
                var results =
                    from c in context.Clients
                    join l in context.LoanMasters
                    on c.ClientId equals l.ClientId
                    where c.LastName == lastName

                    //select new {c, l};
                    //select new GetLoanInfo(){FirstName = c.FirstName, LastName = c.LastName, c.ClientId, l.LoanMasterId, l.DateCreated};
                    select new {FirstName = c.FirstName, LastName = c.LastName, ClientId = c.ClientId, LoanMasterId = l.LoanMasterId, DateCreated = l.DateCreated};
                    //select FirstName, LastName;

                    //or use lambda =>
                    //context.Clients.Join(context.LoanMasters, c => c.ClientId, l => l.ClientId,
                    //    (c, l) => new {FirstName = c.FirstName, LoanId = l.LoanMasterId});

                    //where -- = loanItemId  //Using param                

                //Had issues with converting anonymous to string
                var resultList = results.ToList();
                return resultList;
                //return (IList<string>)resultList;
                //return GetLoanInfo = resultList;


            }
        }
        public IList<Client> GetClientsByName(string searchby)
        {
            IList<Client> allClients = GetAllClients();

            IList<Client> filteredClients = allClients.Where(client =>
                string.Equals(client.LastName, searchby, StringComparison.OrdinalIgnoreCase)).ToList();

            return filteredClients;
        }

    }
}