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
                var timeFramedLoans = context.LoanMasters
                                    .Where(loan => loan.DateCreated >= startDate
                                    && loan.DateCreated <= endDate);


                var numberOfLoans = timeFramedLoans.Count();

                

                var query = from loans in timeFramedLoans
                            join clients in context.Clients 
                            on loans.ClientId 
                            equals clients.ClientID select clients;

                var inventoryQuery = from loans in timeFramedLoans
                                     join inventoryItems in context.InventoryItems
                                     on loans.ClientId
                                     equals inventoryItems.InventoryCategoryId
                                     select inventoryItems;


                var loanDetailQuery = from loans in timeFramedLoans
                                      join loanDetails in context.LoanDetails
                                      on loans.LoanMasterId
                                      equals loanDetails.LoanMasterId
                                      select loanDetails;




                // Created variables that hold the total amount of loans per type of borrower

                var borrowerWithDisability = query.Where(borrower => borrower.ClientCategoryId == 1).Count();
                var borrowerFamily = query.Where(borrower => borrower.ClientCategoryId == 2).Count();
                var borrowerEducation = query.Where(borrower => borrower.ClientCategoryId == 3).Count();
                var borrowerEmployment = query.Where(borrower => borrower.ClientCategoryId == 4).Count();
                var borrowerHealth = query.Where(borrower => borrower.ClientCategoryId == 5).Count();
                var borrowerCommunityLiving = query.Where(borrower => borrower.ClientCategoryId == 6).Count();
                var borrowerTechnology = query.Where(borrower => borrower.ClientCategoryId == 7).Count();

                // Found total amount of loans for all borrowers
                var totalBorrowers = borrowerWithDisability + borrowerFamily + borrowerEducation +
                    borrowerEmployment + borrowerHealth + borrowerCommunityLiving + borrowerTechnology;

                // Counts the number of times a category appears
                var categoryCounts = context.getInventoryItemCategoryCount(startDate, endDate);

                // Counts the number of times purpose types appear
                var purposeAssistDecisionMaking = loanDetailQuery.Where(purpose => purpose.PurposeType.Equals("Assist in decision making")).Count();

                // Assigned that value to the view model property
                viewModel.TotalNumberOfLoans = numberOfLoans;

                viewModel.NumberOfDeviceBorrowerWithDisability = borrowerWithDisability;
                viewModel.NumberOfBorrowerFamily = borrowerFamily;
                viewModel.NumberOfBorrowerEducation = borrowerEducation;
                viewModel.NumberOfBorrowerEmployment = borrowerEmployment;
                viewModel.NumberOfBorrowerHealth = borrowerHealth;
                viewModel.NumberOfBorrowerCommunityLiving = borrowerCommunityLiving;
                viewModel.NumberOfBorrowerTechnology = borrowerTechnology;

                viewModel.categoryCounts = categoryCounts;

                viewModel.purposeDecisionMaking = purposeAssistDecisionMaking;

                return viewModel;
            }
            
        }
    }
}
