using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.Entities
{
    public class DisabilityCategory
    {
        [Key]   
        public int DisabilityCategoryId { get; set; }

        public string DisabilityType { get; set; }

    }
}
