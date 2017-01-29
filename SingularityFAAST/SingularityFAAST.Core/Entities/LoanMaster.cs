using System;
using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class LoanMaster
    {

        [Key]
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsActive { get; set; }

        public int ClientId { get; set; }

        public string LoanNumber { get; set; }
    }
}