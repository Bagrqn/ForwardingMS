using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Company;
using FMS.Services.Contracts;
using FMS.Services.Models.Company;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class CompanyTypeService : ICompanyTypeService
    {
        private readonly FMSDBContext data;

        public CompanyTypeService(FMSDBContext data)
        {
            this.data = data;
        }
        public void Create(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of company type can not be null or empty. ");
            }
            if (name.Length > DataValidation.CompanyTypeNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of company type can not be longer then {DataValidation.CompanyTypeNameMaxLenght} charecters. ");
            }
            if (data.CompanyTypes.Any(ct => ct.Name == name))
            {
                throw new InvalidOperationException($"Name of company type already exist. ");
            }

            var companytype = new CompanyType()
            {
                Name = name,
                Description = description
            };

            this.data.CompanyTypes.Add(companytype);
            this.data.SaveChanges();
        }

        public ICollection<int> GetIDs()
        {
            return this.data.CompanyTypes.Select(ct => ct.ID).ToList();
        }

        public IEnumerable<CompanyTypeListingModel> FindByName(string name)
        {
            return data.CompanyTypes
                .Where(ct => ct.Name.ToLower().Contains(name.ToLower()))
                .Select(ct => new CompanyTypeListingModel()
                {
                    ID = ct.ID,
                    Name = ct.Name,
                    Description = ct.Description
                });
        }

        public IEnumerable<CompanyTypeListingModel> ListAll()
        {
            return data.CompanyTypes
                .Select(ct => new CompanyTypeListingModel
                {
                    ID = ct.ID,
                    Name = ct.Name,
                    Description = ct.Description
                });
        }
    }
}
