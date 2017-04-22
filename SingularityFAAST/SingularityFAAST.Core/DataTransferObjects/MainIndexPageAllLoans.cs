using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.Core.DataTransferObjects
{
    public class MainIndexPageAllLoans
    {
        //Loan Master
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }

        public int ClientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }
    }
}
