using System.Data.Entity;
using SingularityFAAST.Core.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using SingularityFAAST.Core.DataTransferObjects;
using System.Data.SqlClient;

namespace SingularityFAAST.DataAccess.Contexts
{
    
    public class SingularityDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientDisability> ClientDisabilities { get; set; }
        public DbSet<DisabilityCategory> DisabilityCategories { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }


        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<LoanMaster> LoanMasters { get; set; }
        public DbSet<LoanDetail> LoanDetails { get; set; }

        public IList<ReportInventoryItemCategoryCount> getInventoryItemCategoryCount(DateTime startDate, DateTime endDate)
        {
            var parameters = new[]
            {
                new SqlParameter("@startDate", startDate),
                new SqlParameter("@endDate", endDate)
            };

            IList<ReportInventoryItemCategoryCount> categoryCounts = this.Database.SqlQuery<ReportInventoryItemCategoryCount>
                ("dbo.InventoryItemCategoryCount @startDate, @endDate",
                parameters).ToList();
            return categoryCounts;
            
        }
    }

}
