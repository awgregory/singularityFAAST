using System;
using System.Collections.Generic;
using System.Linq;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;

namespace SingularityFAAST.Services.Services
{
    public class ReportingServices
    {
        private readonly SingularityDBContext _dbContext = new SingularityDBContext();

        public IList<Loan> GetAllLoans()
        {
            IList<Loan> loans = _dbContext.Loans.ToList();

            //var allActiveLoans = loans.Where(loan => loan.IsActive == true);

            //IList<bool> oldLoans = loans
            //    .Where(loan => loan.DateCreated < DateTime.UtcNow)
            //    .Select(filteredLoan => filteredLoan.IsActive)
            //    .ToList();

            return loans;
        }

        public IList<Loan> GetDateFilteredLoans(DateTime startDate, DateTime endDate)
        {
            IList<Loan> filteredLoans = _dbContext.Loans
                .Where(loan => loan.DateCreated > startDate
                            && loan.DateCreated < endDate)
                .ToList();

            return filteredLoans;
            ;
        }
    }
}
