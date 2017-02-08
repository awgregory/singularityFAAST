using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.Entities
{
    public class ClientDisability
    {
        [Key]
        [Column(Order = 1)]
        public int DisabilityCategoryId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ClientId { get; set; }

    }
}
