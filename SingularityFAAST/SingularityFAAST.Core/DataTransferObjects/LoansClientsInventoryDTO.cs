using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class LoansClientsInventoryDTO
    {
        //Loan Master
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }


        //LoanDetail
        public int LoanDetailId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        public int InventoryItemId { get; set; }

        public string Notes { get; set; }


        //Client
        public int ClientId { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }


        //Inventory
        [Key]
        public DateTime DatePurchased { get; set; }
        public int InventoryCategoryId { get; set; }
        public string ItemName { get; set; }
        public string Manufacturer { get; set; }
        public bool Availability { get; set; }



    }
}