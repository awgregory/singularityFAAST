using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class InventoryItem
    {
        [Key]
        public int InventoryItemId { get; set; }
        public DateTime DatePurchased { get; set; }
        public int InventoryCategoryId { get; set; }
        public string ItemName { get; set; }
        public decimal PricePaid { get; set; }
        public decimal RetailCost { get; set; }
        public string ModelName { get; set; }
        public string Location { get; set; }
        public bool Availability { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Accessories { get; set; }
        public string Damages { get; set; }
        public string Manufacturer { get; set; }
    }
}
