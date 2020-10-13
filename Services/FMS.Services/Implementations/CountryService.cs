using FMS.Data;
using FMS.Data.Models;
using FMS.Services.Models.Country;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly FMSDBContext data;

        //Constructor - Dependency Inversion Principle
        public CountryService(FMSDBContext data)
            => this.data = data;

        public void Create(string name)
        {
            if (name.Length > DataValidation.CountryNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of country can not be longer then {DataValidation.CountryNameMaxLenght} characters. ");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of country can not be null or empty. ");
            }
            if (data.Countries.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                throw new InvalidOperationException($"Country: \"{name}\" already exist ");
            }
            var country = new Country
            {
                Name = name
            };
            this.data.Countries.Add(country);
            this.data.SaveChanges();
        }

        public ICollection<int> GetAllIDs()
        {
            return data.Countries.Select(c => c.ID).ToList();
        }

        public int Count()
        {
            return this.data.Countries.Count();
        }

        public Country GetUndefined()
        {
            var country = data.Countries.FirstOrDefault(c => c.Name == "Undefined");
            if (country == null)
            {
                data.Countries.Add(new Country()
                {
                    Name = "Undefined"
                });
                data.SaveChanges();
                return data.Countries.FirstOrDefault(c => c.Name == "Undefined");
            }
            return country;
        }
        public IEnumerable<CountryListingServiceModel> SearchByName(string name)
        {
            return data.Countries
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new CountryListingServiceModel
                {
                    ID = c.ID,
                    Name = c.Name
                })
                .ToList();

        }

        public IEnumerable<CountryListingServiceModel> GetAll()
        {
            return data.Countries
                .Select(c => new CountryListingServiceModel
                {
                    ID = c.ID,
                    Name = c.Name
                })
                .ToList();
        }
    }
}
