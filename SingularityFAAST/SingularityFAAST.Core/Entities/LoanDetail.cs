using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class LoanDetail
    {
        [Key]
        public int LoanDetailId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        public string Purpose { get; set; }

        public string PurposeType { get; set; }

        public string ClientOutcome { get; set; }

        public string Notes { get; set; }

        public int InventoryItemId { get; set; }
    }
}
