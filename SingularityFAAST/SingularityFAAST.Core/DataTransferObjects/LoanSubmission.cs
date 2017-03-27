using SingularityFAAST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class LoanSubmission
    {
        //TODO: add information as necessary
        //for now these are the starting points of the New Loan page 
        public int ClientId { get; set; }   //id not ID

        public IEnumerable<int> InventoryItemIds { get; set; }


        //add the 3 values for the 3 drop downs
        public string Type { get; set; }   //or change these

        public string Purpose { get; set; }

        public string PurposeType { get; set; }

    }
}