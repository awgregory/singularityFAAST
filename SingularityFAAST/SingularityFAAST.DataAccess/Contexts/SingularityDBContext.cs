using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Net.Http;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.DataAccess.Contexts
{
    // ReSharper disable once InconsistentNaming
    public class SingularityDBContext : DbContext
    {
        //TODO: fill out entities (with [Key]!)
        public DbSet<Client> Clients { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Loan> Loans { get; set; }

        //Nick - example reference code
        public DbSet<Dummy> Dummies { get; set; }
    }
}
