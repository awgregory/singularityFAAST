using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.Entities
{
    public class ClientCategory
    {
        [Key]
        public int ClientCategoryId { get; set; }

        public string Type { get; set; }

    }
}
