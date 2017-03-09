using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class LoansClientsInventoryDTO //: IEnumerable
    {
        //Loan Master
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }

        public IEnumerable<int> LoanMasterIds { get; set; }


        //LoanDetail
        public int LoanDetailId { get; set; }

        public DateTime LoanDate { get; set; }

        public int LoanDuration { get; set; }

        public int InventoryItemId { get; set; }

        public string ClientOutcome { get; set; }

        public string Notes { get; set; }

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

        //ClientCategories
        public string Type { get; set; }


        public IEnumerable<string> ClientCategory { get; set; }

        public List<InventoryItem> OrderData { get; set; }
        public List<Client> OrderDetailData { get; set; }

        //[Display(Name = "Client Category")]
        //public ClientCategory ClientCategory { get; set; }


        //public SelectList ClientCategorySelectList { get; set; } 

        //public SelectList PurposeSelectList { get; set; }

        //public SelectList PurposeTypeSelectList { get; set; }


        //public LoansClientsInventoryDTO()
        //{
        //    this.LoanNumber = LoanNumber;
        //    this.Accessories = Accessories;
        //    this.Active = Active;
        //    this.Availability = Availability;
        //    this.ClientCategoryId = ClientCategoryId;
        //    this.ClientId = ClientId;
        //    this.Type = Type;
        //    this.Damages = Damages;
        //    this.DateCreated = DateCreated;
        //    this.DatePurchased = DatePurchased;
        //    this.Description = Description;
        //    this.Email = Email;
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;
        //    this.HomePhone = HomePhone;
        //    this.InventoryCategoryId = InventoryCategoryId;
        //    this.InventoryItemId = InventoryItemId;
        //    this.IsActive = IsActive;
        //    this.ItemName = ItemName;
        //    this.LoanDate = LoanDate;
        //    this.LoanDetailId = LoanDetailId;
        //    this.LoanDuration = LoanDuration;
        //    this.LoanEligibility = LoanEligibility;
        //    this.LoanMasterId = LoanMasterId;
        //    this.Manufacturer = Manufacturer;
        //    this.MiddleInitial = MiddleInitial;
        //    this.Notes = Notes;
        //    this.Purpose = Purpose;
        //    this.PurposeType = PurposeType;
        //    this.Type = Type;
        //}


        public IEnumerable<int> ClientCategoryID { get; set; }


    }
}