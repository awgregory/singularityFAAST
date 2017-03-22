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
        //Constructor
        public EditClientViewModel(Client client,
        IEnumerable<DisabilityCategory> disabilityCategories,
        IEnumerable<LoanMaster> associatedLoans)
        {
            this.Client = client;       // this refers to the class which you are currently writing code in
            DisabilityCategories = disabilityCategories;
            AssociatedLoans = associatedLoans;
        }

        //Client
        public Client Client { get; set; }

        //DisabilityCategories
        public IEnumerable<DisabilityCategory> DisabilityCategories { get; set; }

        //Loans
        public IEnumerable<LoanMaster> AssociatedLoans { get; set; }

    }
}

// Like the second kind of view model, for wrapping two kinds of domain model classes in one container 
// Client and sequence of DisabilityCategory objects   (or maybe of Hybrid of these two) hmmm

// DTO more like first kind, using select properties of other objects