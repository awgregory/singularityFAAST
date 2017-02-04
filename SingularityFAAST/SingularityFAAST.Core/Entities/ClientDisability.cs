using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.Entities
{
    public class ClientDisability
    {
        [Key]
        public int DisabilityCategoryId { get; set; }
        [Key]
        public int ClientId { get; set; }

    }
}
