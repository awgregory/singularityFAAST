using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class LoansClientsInventoryDTO 
    {
        public string NullDisplayText { get; set; }

        //Loan Master
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }

        public IEnumerable<int> LoanMasterIds { get; set; }

        public string ClientOutcome { get; set; }

        public string LoanNotes { get; set; }

        public bool IsDeleted { get; set; }


        //LoanDetail
        public int LoanDetailId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        public int InventoryItemId { get; set; }

        public string Purpose { get; set; }

        public string PurposeType { get; set; }

        public IEnumerable<int> LoanDetailIds { get; set; }


        //Client
        public int ClientId { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string LastName { get; set; }

        public string HomePhone { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public bool LoanEligibility { get; set; }

        public int ClientCategoryId { get; set; }



        //Inventory
        public DateTime DatePurchased { get; set; }
        public int InventoryCategoryId { get; set; }
        public string ItemName { get; set; }
        public string Manufacturer { get; set; }
        public bool Availability { get; set; }
        public string Description { get; set; }
        public string Damages { get; set; }
        public string Accessories { get; set; }
        public IEnumerable<int> InventoryItemIds { get; set; }
        public List<InventoryItem> InventoryItems { get; set; }

    }
}