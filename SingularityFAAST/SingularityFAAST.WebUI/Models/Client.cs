using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SingularityFAAST.WebUI.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        //public string Company { get; set; } // Keep? All seem empty
        public string Address { get; set; }
        public string StateAbbr { get; set; }
        public int ZipCode { get; set; }
        public string Category { get; set; }
    }
}