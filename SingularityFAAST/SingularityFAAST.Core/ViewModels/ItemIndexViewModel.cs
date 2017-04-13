using System;
using System.Collections.Generic;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.Core.ViewModels
{
    public class ItemIndexViewModel
    {
        public IEnumerable<InventoryItem> InventoryItems { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
