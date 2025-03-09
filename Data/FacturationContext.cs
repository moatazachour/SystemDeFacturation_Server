using Microsoft.EntityFrameworkCore;
using SystemDeFacturation_Server.Models;

namespace SystemDeFacturation_Server.Data
{
    public class FacturationContext : DbContext
    {
        public DbSet<Acheteur> Acheteurs => Set<Acheteur>();
        public DbSet<Facture> Factures => Set<Facture>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("""
                Data Source=localhost;
                Initial Catalog=SysFacturationDB;
                User Id=sa;
                Password=sa123456;
                TrustServerCertificate=True;
                """);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
