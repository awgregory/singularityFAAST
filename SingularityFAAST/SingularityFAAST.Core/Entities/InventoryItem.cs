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
        public string Manufacturer { get; set; }
        public decimal PricePaid { get; set; }
        public decimal RetailCost { get; set; }
        public string ModelName { get; set; }
        public string Location { get; set; }
        public bool Availability { get; set; }
        public bool Active { get; set; }
        public string SerialNumber { get; set; }
        public string Description { get; set; }
        public string Accessories { get; set; }
        public string Damages { get; set; }


        //Data Annotations Display would still require logic, DisplayFor is more static for single values
        public string InventoryCategoryName
        {
            get
            {
                string inventoryCategory;

                switch (InventoryCategoryId)
                {
                    case 1:
                        inventoryCategory = "Computers and Related Technology";
                        break;
                    case 2:
                        inventoryCategory = "Daily Living";
                        break;
                    case 3:
                        inventoryCategory = "Environmental Adaptations";
                        break;
                    case 4:
                        inventoryCategory = "Hearing";
                        break;
                    case 5:
                        inventoryCategory = "Leaning, Cognition, and Development";
                        break;
                    case 6:
                        inventoryCategory = "Mobility, Seating, and Positioning";
                        break;
                    case 7:
                        inventoryCategory = "Recreational, Sports, and Leisure";
                        break;
                    case 8:
                        inventoryCategory = "Speech Communication";
                        break;
                    case 9:
                        inventoryCategory = "Vehicle Mods and Transport";
                        break;
                    default:
                        inventoryCategory = "Vision";
                        break;
                }

                return inventoryCategory;
            }
        }
    }
}