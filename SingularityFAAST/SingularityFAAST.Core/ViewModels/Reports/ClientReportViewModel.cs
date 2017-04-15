using SingularityFAAST.Core.DataTransferObjects;
using SingularityFAAST.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.ViewModels.Reports
{
    public class ClientReportViewModel
    {
        public IList<Client> clientList { get; set; }
    }
}
