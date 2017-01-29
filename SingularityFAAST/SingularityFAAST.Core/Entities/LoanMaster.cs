using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class LoanMaster
    {
        //[Key]
        //public int LoanDetailId { get; set; }

        [Key]
        public int LoanMasterId { get; set; }

        //public DateTime LoanDate{ get; set; }

        //public int LoanDuration { get; set; }

        //public string Purpose { get; set; }

        //public string PurposeType { get; set; }

        // public string ClientOutcome { get; set; }

        //public string Notes { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public int ClientId { get; set; }

        public int LoanCategoryId { get; set; }

        public int InventoryItemId { get; set; }

        //public int InventoryItemId1 { get; set; }

        //public int InventoryItemId2 { get; set; }

        //public int InventoryItemId3 { get; set; }

        //public int InventoryItemId4 { get; set; }

        //public int InventoryItemId5 { get; set; }

        //public int InventoryItemId6 { get; set; }

        public string LoanNumber { get; set; }
    }
}