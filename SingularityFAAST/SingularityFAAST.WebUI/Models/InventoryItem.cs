using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingularityFAAST.WebUI.Models
{
    /**InventoryItem contains item id, date purchased, Category Id (FK), item name, 
     * price paid, retail cost, model name, location, serial number, description, 
     * accessories, damages, and availability for and device at Hope Haven's FAAST Demonstration center**/

    public class InventoryItem
    {
        public int  InventoryItemId { get; set; }
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
    }
}