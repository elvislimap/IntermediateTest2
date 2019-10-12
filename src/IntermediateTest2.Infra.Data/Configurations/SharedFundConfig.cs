using IntermediateTest2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntermediateTest2.Infra.Data.Configurations
{
    public class SharedFundConfig : IEntityTypeConfiguration<SharedFund>
    {
        public void Configure(EntityTypeBuilder<SharedFund> builder)
        {
            builder.HasKey(s => s.SharedFundId);
            builder.Property(s => s.SharedFundId).HasColumnType("int").ValueGeneratedOnAdd();

            builder.Property(s => s.EmployeeId).HasColumnType("int").IsRequired();
            builder.Property(s => s.Value).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(s => s.ContributionDate).HasColumnType("datetime").IsRequired();

            builder.HasOne(s => s.Employee).WithMany(e => e.SharedFunds);
        }
    }
}