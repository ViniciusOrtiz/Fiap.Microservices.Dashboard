using GeekBurguer.Dashboard.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer.Dashboard.Api.Infra
{
    public class DashboardContext : DbContext
    {
        public DashboardContext(DbContextOptions<DashboardContext> options, DbSet<User> user, DbSet<SaleResponse> salesResponse, DbSet<SaleRequest> salesRequest)
            : base(options)
        {
            User = user;
            SalesResponse = salesResponse;
            SalesRequest = salesRequest;
        }

        public DbSet<User> User { get; set; }
        public DbSet<SaleResponse> SalesResponse { get; set; }
        public DbSet<SaleRequest> SalesRequest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}