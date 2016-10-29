using System.ComponentModel.DataAnnotations;

namespace SingularityFAAST.Core.Entities
{
    public class Dummy
    {
        [Key]
        public int DummyId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Description { get; set; }
    }
}
