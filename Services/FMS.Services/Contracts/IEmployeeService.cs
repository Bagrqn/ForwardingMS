using System;

namespace FMS.Services.Contracts
{
    public interface IEmployeeService
    {
        void Create(string firstName, string middleName, string lastName, string EGN, DateTime birthDate, int genderID);
    }
}
