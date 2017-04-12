using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Core.Entities;
using System.Collections.Generic;



namespace SingularityFAAST.Core.ViewModels
{
    public class ClientIndexViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
