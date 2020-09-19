using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace FMS.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly FMSDBContext data;

        public EmployeeService(FMSDBContext data)
            => this.data = data;
        public void Create(string firstName, string middleName, string lastName, string EGN, DateTime birthDate, int genderID)
        {
            #region firstNameValidations
            if (string.IsNullOrEmpty(firstName))
            {
                throw new InvalidOperationException("First name can not be null. ");
            }
            if (firstName.Length > DataValidation.EmployeeNameMaxLenght)
            {
                throw new InvalidOperationException($"First name can not longer then {DataValidation.EmployeeNameMaxLenght} characters. ");
            }
            #endregion

            #region middleNameValidations
            if (string.IsNullOrEmpty(middleName))
            {
                throw new InvalidOperationException("Middle name can not be null. ");
            }
            if (middleName.Length > DataValidation.EmployeeNameMaxLenght)
            {
                throw new InvalidOperationException($"Middle name can not longer then {DataValidation.EmployeeNameMaxLenght} characters. ");
            }
            #endregion

            #region lastNameValidations
            if (string.IsNullOrEmpty(lastName))
            {
                throw new InvalidOperationException("Last name can not be null. ");
            }
            if (lastName.Length > DataValidation.EmployeeNameMaxLenght)
            {
                throw new InvalidOperationException($"Last name can not longer then {DataValidation.EmployeeNameMaxLenght} characters. ");
            }
            #endregion

            #region EGNValidation
            if (string.IsNullOrEmpty(EGN))
            {
                throw new InvalidOperationException("EGN name can not be null. ");
            }
            if (EGN.Length > DataValidation.EmployeeNameMaxLenght)
            {
                throw new InvalidOperationException($"EGN can not longer then {DataValidation.EmployeeEGNMaxLenght} characters. ");
            }
            #endregion

            var employee = new Employee
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                EGN = EGN,
                BirthDate = birthDate,
                GenderID = genderID,
            };

            data.Employees.Add(employee);
            data.SaveChanges();
        }
    }
}
