using FMS.Data.Models.Employee;
using FMS.Services.Implementations;

namespace FMS.Services.Factory.Employee
{
    public class EmployeeFactory
    {
        public static FMS.Data.Models.Employee.Employee NewEmployee()
        {
            return new Data.Models.Employee.Employee();
        }
    }
}
