using FMS.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMS.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> employee)
        {
            //Employee witn one gender
            employee.HasOne(e => e.Gender)
            .WithMany(e => e.Employees)
            .HasForeignKey(e => e.GenderID);

            //Employee with many employeeStringProps
            employee.HasMany(e => e.EmployeeStringProps)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeID);

            //Employee with many employeeNumericProps
            employee.HasMany(e => e.EmployeeNumericProps)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeID);

            //Employee with many employeeBoolProps
            employee.HasMany(e => e.EmployeeBoolProps)
            .WithOne(e => e.Employee)
            .HasForeignKey(e => e.EmployeeID);
        }
    }
}
