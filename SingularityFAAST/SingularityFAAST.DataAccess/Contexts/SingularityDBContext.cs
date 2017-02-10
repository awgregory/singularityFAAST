using System.Data.Entity;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.DataAccess.Contexts
{
    // ReSharper disable once InconsistentNaming
    public class SingularityDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<LoanMaster> LoanMasters { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }
    }

}
