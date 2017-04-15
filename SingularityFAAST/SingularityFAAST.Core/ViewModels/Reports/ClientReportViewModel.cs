using SingularityFAAST.Core.DataTransferObjects;
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
        public IList<ReportClientList> clientList { get; set; }
    }
}
