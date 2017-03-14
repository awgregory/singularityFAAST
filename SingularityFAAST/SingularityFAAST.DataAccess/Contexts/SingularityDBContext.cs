using System.Data.Entity;
using SingularityFAAST.Core.Entities;

namespace SingularityFAAST.DataAccess.Contexts
{
    
    public class SingularityDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDisability> ClientDisabilities { get; set; }
        public DbSet<DisabilityCategory> DisabilityCategories { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }
        public DbSet<User> UserLogIns { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryItemCategory> InventoryCategories { get; set; }
        public DbSet<LoanMaster> LoanMasters { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }
    }

}
