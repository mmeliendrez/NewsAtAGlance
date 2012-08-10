using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using AccountAtAGlance.Model;

namespace NewsAtAGlance.Repository
{
    public class NewsAtAGlance: DbContext
    {
        public DbSet<BrokerageAccount> BrokerageAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<MarketIndex> MarketIndices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderType> OrderTypes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<MutualFund> MutualFunds { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Mapeo a tablas
            modelBuilder.Entity<Security>().ToTable("Securities");
            modelBuilder.Entity<Stock>().ToTable("Securities_Stock");
            modelBuilder.Entity<MutualFund>().ToTable("Securities_MutualFunds");

            modelBuilder.Entity<WatchList>()                    // Existe una relación N a N entre Securities y WatchLists
                .HasMany(w => w.Securities).WithMany()
                .Map(map => map.ToTable("WatchListSecurity")    // Entonces se crea la tabla intermedia WatchListSecurity
                    .MapRightKey("SecurityId")      // Indicamos las claves extranjeras en WatchListSecurity que harán referencia
                    .MapLeftKey("WatchListId"));    // a la claves primarias de Securities y WatchList respectivamente. 

        }


        // Store procedures
        public int DeleteAccounts()
        {
            return base.Database.ExecuteSqlCommand("DeleteAccounts");   // Una manera de llamar a los Stored Procedures
            // base: dbContext
            // Database: una propiedad del dbContext
            // ExecuteSqlCommand: devuelve el número de filas afectadas
        }

        public int DeleteSecuritiesAndExchanges()
        {
            return base.Database.ExecuteSqlCommand("DeleteSecuritiesAndExchanges");  
        }
    }
}
