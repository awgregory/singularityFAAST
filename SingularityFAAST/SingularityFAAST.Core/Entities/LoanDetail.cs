using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingularityFAAST.Core.Entities
{
    public class LoanDetail
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int LoanDetailId { get; set; }

        [Key, Column(Order =1)]
        public int LoanMasterId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        [Key, Column(Order = 4)]
        public int InventoryItemId { get; set; }

        public string Purpose { get; set; }

        public string PurposeType { get; set; }

        public string ClientOutcome { get; set; }

        public string Notes { get; set; }

        public IEnumerable<int> LoanDetailIds { get; set; }
    }
}
