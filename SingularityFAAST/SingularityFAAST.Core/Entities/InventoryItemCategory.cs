using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class InventoryItemCategory
    {
        [Key]
        public int InventoryCategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
