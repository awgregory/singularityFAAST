using System;
using System.Collections.Generic;
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

    }
}
