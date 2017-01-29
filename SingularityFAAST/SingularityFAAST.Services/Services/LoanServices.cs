using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SingularityFAAST.Services.Services
    { 
     public class LoanServices           
		{
        public IList<Loan> GetAllLoans()
         {
              using (var context = new SingularityDBContext())
              {
                  var loans = context.LoanMaster;
                  
                  var loanList = loans.ToList();
                  
                  return loanList;
              }
          }
  
          public void SaveLoan(Loan loan)
          {
              using (var context = new SingularityDBContext())
              {
                  loan.DateCreated = DateTime.Now;  // Manipulating the client object is done before saving to Db
  
                  context.LoanMaster.Add(LoanMaster);
  
                  var rowsAffected = context.SaveChanges();
              }
          }
     }
}
