using SingularityFAAST.Core.Entities;
using System.Collections.Generic;

namespace SingularityFAAST.Core.ViewModels
{
    public class UpdateInventoryItemViewModel
    {
        public InventoryItem InventoryItem { get; set; }

        public int NextInventoryNumber { get; set; } //added int property to page

        public IList<InventoryItemCategory> InventoryItemCategories { get; set; }
    }
}
