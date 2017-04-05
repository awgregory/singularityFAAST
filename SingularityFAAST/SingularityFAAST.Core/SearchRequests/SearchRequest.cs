using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.SearchRequests
{
    public class SearchRequest
    {
        public string SearchBy { get; set; }


        // To Search Service Switch Statement
        public int SearchByType { get; set; }

    }
}


    // Like the first kind of view model, a convenient class for passing specific data between controller and view


    // Not persisted in db like domain model Entities