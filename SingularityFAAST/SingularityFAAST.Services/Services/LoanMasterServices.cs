using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
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


    }
}