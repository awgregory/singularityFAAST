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

                // Created variables that hold the total amount of loans per type of borrower

                //var borrowerWithDisability = timeFramedLoans.Where(borrower => borrower.ClientCategoryId == 1);

                var borrowerWithDisability = context.Clients.Count(borrower => borrower.ClientCategoryId == 1);
                var borrowerFamily = context.Clients.Count(borrower => borrower.ClientCategoryId == 2);
                var borrowerEducation = context.Clients.Count(borrower => borrower.ClientCategoryId == 3);
                var borrowerEmployment = context.Clients.Count(borrower => borrower.ClientCategoryId == 4);
                var borrowerHealth = context.Clients.Count(borrower => borrower.ClientCategoryId == 5);
                var borrowerCommunityLiving = context.Clients.Count(borrower => borrower.ClientCategoryId == 6);
                var borrowerTechnology = context.Clients.Count(borrower => borrower.ClientCategoryId == 7);

                // Found total amount of loans for all borrowers
                var totalBorrowers = borrowerWithDisability + borrowerFamily + borrowerEducation +
                    borrowerEmployment + borrowerHealth + borrowerCommunityLiving + borrowerTechnology;

                // Assigned that value to the view model property
                viewModel.TotalNumberOfLoans = numberOfLoans;

                viewModel.NumberOfDeviceBorrowerWithDisability = borrowerWithDisability;
                viewModel.NumberOfBorrowerFamily = borrowerFamily;
                viewModel.NumberOfBorrowerEducation = borrowerEducation;
                viewModel.NumberOfBorrowerEmployment = borrowerEmployment;
                viewModel.NumberOfBorrowerHealth = borrowerHealth;
                viewModel.NumberOfBorrowerCommunityLiving = borrowerCommunityLiving;
                viewModel.NumberOfBorrowerTechnology = borrowerTechnology;

                return viewModel;
            }
            
        }
    }
}
