using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingularityFAAST.Core.Entities
{
    public class LoanDetail
    {
        [Key, Column(Order = 0)]
        public int LoanDetailId { get; set; }

        [Key, Column(Order =1)]
        public int LoanMasterId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        public string Purpose { get; set; }

        public string PurposeType { get; set; }

        public string ClientOutcome { get; set; }

        public string Notes { get; set; }

        public int InventoryItemId { get; set; }
    }
}
