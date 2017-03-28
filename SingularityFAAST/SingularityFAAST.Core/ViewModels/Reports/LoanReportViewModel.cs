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
        public int TotalNumberOfLoans { get; set; }

        // Create Variables for Device Loans for Borrower Type
        public int NumberOfDeviceBorrowerWithDisability { get; set; }
        public int NumberOfBorrowerFamily { get; set; }
        public int NumberOfBorrowerEducation { get; set; }
        public int NumberOfBorrowerEmployment { get; set; }
        public int NumberOfBorrowerHealth { get; set; }
        public int NumberOfBorrowerCommunityLiving { get; set; }
        public int NumberOfBorrowerTechnology { get; set; }

        // Create variable for the counts of InventoryItemCateogory
        public IList<ReportInventoryItemCategoryCount> categoryCounts { get; set; }
    }
}
