using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingularityFAAST.Core.Entities
{
    public class LoanMaster
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanMasterId { get; set; }

        public DateTime DateCreated { get; set; }

        [Key, Column(Order = 2)]
        public int ClientId { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool IsActive { get; set; }

        public string LoanNumber { get; set; }

        public bool IsDeleted { get; set; }

        public string ClientOutcome { get; set; }

        public string LoanNotes { get; set; }
        //public IEnumerable<int> LoanMasterIds { get; set; }
    }
}