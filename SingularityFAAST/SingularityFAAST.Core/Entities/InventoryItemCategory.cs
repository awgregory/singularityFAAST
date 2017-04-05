using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.Entities
{
    public class InventoryItemCategory
    {
        [Key]
        public int InventoryCategoryId { get; set; }

        public string Category { get; set; }
    }
}
