using IntermediateTest2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntermediateTest2.Infra.Data.Configurations
{
    public class InflationAdjustConfig : IEntityTypeConfiguration<InflationAdjust>
    {
        public void Configure(EntityTypeBuilder<InflationAdjust> builder)
        {
            builder.HasKey(i => i.InflationAdjustId);
            builder.Property(i => i.InflationAdjustId).HasColumnType("int").ValueGeneratedOnAdd();

            builder.Property(i => i.Percentage).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(i => i.AdjustmentDate).HasColumnType("datetime").IsRequired();
        }
    }
}