using System;
using System.Collections.Generic;
using System.Linq;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.DataAccess.Contexts;
using SingularityFAAST.Core.ViewModels.Reports;

namespace SingularityFAAST.Services.Services
{
    public class ReportingServices
    {
        private readonly SingularityDBContext _dbContext = new SingularityDBContext();
    
        public LoanReportViewModel CreateLoanReportViewModel(DateTime startDate, DateTime endDate)
        {
            using (var context = new SingularityDBContext())
            {
                // We created a view model object that we will populate with information
                var viewModel = new LoanReportViewModel();

                // Created variable that holds the total amount of loans between two dates
                var numberOfLoans = context.LoanMasters
                    .Count(loan => loan.DateCreated >= startDate 
                    && loan.DateCreated <= endDate);

                

                // Assigned that value to the view model property
                viewModel.TotalNumberOfLoans = numberOfLoans;

                return viewModel;
            }
            
        }
    }
}
