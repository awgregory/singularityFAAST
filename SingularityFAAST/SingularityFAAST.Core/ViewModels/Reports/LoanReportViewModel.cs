using SingularityFAAST.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.ViewModels.Reports
{
    public class LoanReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int TotalNumberOfLoans { get; set; }

        // Create Variables for Device Loans for Borrower Type
        public int NumberOfDeviceBorrowerWithDisability { get; set; }
        public int NumberOfBorrowerFamily { get; set; }
        public int NumberOfBorrowerEducation { get; set; }
        public int NumberOfBorrowerEmployment { get; set; }
        public int NumberOfBorrowerHealth { get; set; }
        public int NumberOfBorrowerCommunityLiving { get; set; }
        public int NumberOfBorrowerTechnology { get; set; }
        public int NumberOfTotalBorrowers { get; set; }

        // Create variable for the counts of InventoryItemCateogory
        public IList<ReportInventoryItemCategoryCount> categoryCounts { get; set; }

        // Create variable for the count for each purpose type
        public int PurposeDecisionMaking { get; set; }
        public int PurposeServeAsLoaner { get; set; }
        public int PurposeShortTerm { get; set; }
        public int PurposeConductTraining { get; set; }

    }
}
