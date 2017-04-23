using System.Collections.Generic;
using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Core.Entities;
using SingularityFAAST.Core.SearchRequests;

namespace SingularityFAAST.Core.ViewModels
{
    public class LoanIndexViewModel
    {
        //public IEnumerable<LoanMaster> LoanMasters { get; set; }

        public IEnumerable<LoansClientsInventoryDTO> LoansPage { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
