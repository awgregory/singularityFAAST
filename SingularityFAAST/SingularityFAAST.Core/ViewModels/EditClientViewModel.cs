using SingularityFAAST.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityFAAST.Core.ViewModels
{
    public class EditClientViewModel
    {
        public EditClientViewModel(Client client,
        IEnumerable<DisabilityCategory> disabilityCategories)
        {
            Client = client;
            DisabilityCategories = disabilityCategories;
        }

        public Client Client { get; set; }

        public IEnumerable<DisabilityCategory> DisabilityCategories { get; set; }
    }
}
