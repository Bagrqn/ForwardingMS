using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Company;
using System;

namespace FMS.Services.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly FMSDBContext data;

        public CompanyService(FMSDBContext data)
        {
            this.data = data;
        }

        public void Create(string name, string bulstat, string taxnumber, int countryID, int cityID, int companyTypeID, string address)
        {
            if (name.Length > DataValidation.CompanyNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of company can not be longer than {DataValidation.CompanyNameMaxLenght} characters. ");
            }
            if (bulstat.Length > DataValidation.CompanyBulstatMaxLenght)
            {
                throw new InvalidOperationException($"Bulstat can not be longer than {DataValidation.CompanyBulstatMaxLenght} characters. ");
            }
            if (taxnumber.Length > DataValidation.CompanyTaxNumberMaxLenght)
            {
                throw new InvalidOperationException($"Tax number can not be longer than {DataValidation.CompanyTaxNumberMaxLenght} characters. ");
            }
            if (address.Length > DataValidation.CompanyAddresMaxLenght)
            {
                throw new InvalidOperationException($"Address of company can not be longer than {DataValidation.CompanyTaxNumberMaxLenght} characters. ");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of company can not be null or empty. ");
            }

            var company = new Company
            {
                Name = name,
                Bulstat = bulstat,
                TaxNumber = taxnumber,
                Address = address,
                CountryID = countryID,
                CityID = cityID,
                CompanyTypeID = companyTypeID
            };
            this.data.Companies.Add(company);
            this.data.SaveChanges();
        }
    }
}