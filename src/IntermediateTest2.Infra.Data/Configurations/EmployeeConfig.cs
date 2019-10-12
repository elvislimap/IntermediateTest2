using IntermediateTest2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IntermediateTest2.Infra.Data.Configurations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId).HasColumnType("int").ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(e => e.BirthDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.MonthlySalary).HasColumnType("decimal(10,2)").IsRequired();
        }
    }
}