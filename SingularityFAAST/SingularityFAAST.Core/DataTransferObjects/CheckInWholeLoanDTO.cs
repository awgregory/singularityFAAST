using System;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class CheckInWholeLoanDTO
    {
        public string ItemDamages { get; set; }
        public string ClientOutcome { get; set; }
        public string LoanNotes { get; set; }
        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }
    }
}
