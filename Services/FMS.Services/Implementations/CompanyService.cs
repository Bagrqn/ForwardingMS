using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Company;
using FMS.Data.Models.Request;
using FMS.Services.Contracts;
using FMS.Services.Models.Company;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public RequestToCompanyRelationType GetRequestToCompanyRelationType(string relTypeName)
        {
            return data.RequestToCompanyRelationTypes.FirstOrDefault(x => x.Name == relTypeName);
        }

        public void Delete(int id)
        {
            if (!data.Companies.Any(c => c.ID == id))
            {
                throw new InvalidOperationException($"Company did not exist. ");
            }

            var company = data.Companies.First(c => c.ID == id);
            //Factory ??? Interface? Best way to do this? 
            var deletedProp = Factory.BooleanPropFactory.CreateCompanyBooleanPropetry("Deleted", true, id);

            company.CompanyBoolProps.Add(deletedProp);
            this.data.SaveChanges();
        }

        public CompanyListingServiceModel GetCompany(int id)
        {
            var company = data.Companies.FirstOrDefault(c => c.ID == id);
            var stringProps = data.CompanyStringProps.Where(p => p.CompanyID == id).ToList();
            var numericProps = data.CompanyNumericProps.Where(p => p.CompanyID == id).ToList();

            var result = new CompanyListingServiceModel()
            {
                ID = company.ID,
                Address = company.Address,
                Bulstat = company.Bulstat,
                Name = company.Name,
                TaxNumber = company.TaxNumber,
                CityID = company.CityID,
                CountryID = company.CountryID,
                CompanyTypeID = company.CompanyTypeID
            };
            result.NumericProps = numericProps;
            result.StringProps = stringProps;

            ;
            return result;
        }

        public IEnumerable<CompanyListingServiceModel> GetCarrierCompanies()
        {
            var list = data.Companies.Where(c => c.CompanyTypeID == GetTypeCarrierID()).Select(c => new CompanyListingServiceModel
            {
                ID = c.ID,
                Name = c.Name,
                Address = c.Address,
                Bulstat = c.Bulstat,
                TaxNumber = c.TaxNumber,
                CountryID = c.CountryID,
                CityID = c.CityID,
                CompanyTypeID = c.CompanyTypeID
            }).ToList();
            ;
            return list;
        }

        public IEnumerable<CompanyListingServiceModel> GetPayerCompanies()
        {
            var list = data.Companies.Where(c => c.CompanyTypeID == GetCompanyTypeID("Client")).Select(c => new CompanyListingServiceModel
            {
                ID = c.ID,
                Name = c.Name,
                Address = c.Address,
                Bulstat = c.Bulstat,
                TaxNumber = c.TaxNumber,
                CountryID = c.CountryID,
                CityID = c.CityID,
                CompanyTypeID = c.CompanyTypeID
            }).ToList();
            ;
            return list;
        }

        public int GetCompanyTypeID(string v)
        {
            var typeID = data.CompanyTypes.FirstOrDefault(t => t.Name == v);
            if (typeID == null)
            {
                data.CompanyTypes.Add(new CompanyType()
                {
                    Name = v,
                    Description = v
                });
            }
            data.SaveChanges();
            var a = data.CompanyTypes.FirstOrDefault(t => t.Name == v).ID;
            return a;
        }

        public int GetTypeCarrierID()
        {
            var typeID = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Carrier");
            if (typeID == null)
            {
                data.CompanyTypes.Add(new CompanyType()
                {
                    Name = "Carrier",
                    Description = "Превозвач"
                });
            }
            data.SaveChanges();
            var a = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Carrier").ID;
            return a;
        }

        public Company GetUndefined()
        {
            var undefinedCompany = data.Companies.FirstOrDefault(c => c.Name == "Undefined");
            if (undefinedCompany == null)
            {
                data.Companies.Add(new Company()
                {
                    Name = "Undefined",
                    Address = "",
                    Bulstat = "",
                    TaxNumber = "",
                    CountryID = new CountryService(data).GetUndefined().ID,
                    CityID = new CityService(data).GetUndefined().ID,
                    CompanyTypeID = GetUndefinedType()
                });
                data.SaveChanges();
                return data.Companies.FirstOrDefault(c => c.Name == "Undefined");
            }
            return undefinedCompany;

        }

        private int GetUndefinedType()
        {
            var type = data.CompanyTypes.FirstOrDefault(t => t.Name == "Undefined");
            if (type == null)
            {
                data.CompanyTypes.Add(new CompanyType()
                {
                    Name = "Undefined",
                    Description = "Undefined"
                });
                data.SaveChanges();
                return data.CompanyTypes.FirstOrDefault(t => t.Name == "Undefined").ID;
            }
            return type.ID;
        }

        //To do..
        //Search by name 
        //Listing with page 
    }
}