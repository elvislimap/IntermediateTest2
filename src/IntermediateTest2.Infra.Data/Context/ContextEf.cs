using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IntermediateTest2.Infra.Data.Context
{
    public class ContextEf : DbContext
    {
        public ContextEf(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<InflationAdjust> InflationAdjusts { get; set; }
        public DbSet<SharedFund> SharedFunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("IntermediateTest2");

            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new InflationAdjustConfig());
            modelBuilder.ApplyConfiguration(new SharedFundConfig());
        }
    }
}