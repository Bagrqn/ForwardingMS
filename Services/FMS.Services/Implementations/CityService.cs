using FMS.Data;
using FMS.Data.Models;
using FMS.Services.Models.City;
using FMS.Services.Models.Postcode;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly FMSDBContext data;

        public CityService(FMSDBContext data)
        {
            this.data = data;
        }

        public void Create(string name, int countryID)
        {
            if (name.Length > DataValidation.CityNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of city can not be longer than {DataValidation.CityNameMaxLenght} characters. ");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of city can not be null or empty. ");
            }
            if (data.Cities.Any(c => c.Name.ToLower() == name.ToLower()))
            {
                string cityName = data.Cities.FirstOrDefault(c => c.Name.ToLower() == name.ToLower()).Name;
                throw new InvalidOperationException($"City: \"{cityName}\" already exist. ");
            }
            var city = new City
            {
                Name = name,
                CountryID = countryID
            };
            this.data.Cities.Add(city);
            this.data.SaveChanges();
        }

        public ICollection<int> GetAllIDsByCountry(int countryID)
        {
            return this.data.Cities
                .Where(c => c.CountryID == countryID).ToList()
                .Select(c => c.ID).ToList();
        }

        public IEnumerable<CityListingServiceModel> SearchByName(string name)
        {
            return data.Cities
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .Select(c => new CityListingServiceModel
                {
                    ID = c.ID,
                    Name = c.Name
                });
        }

        public ICollection<int> GetAllIDs()
        {
            return this.data.Cities.Select(city => city.ID).ToList();
        }

        public IEnumerable<PostcodeListingServiceModel> GetPostcodes(int cityID)
        {
            return data.Postcodes
                .Where(p => p.CityID == cityID)
                .Select(p => new PostcodeListingServiceModel()
                {
                    ID = p.ID,
                    Code = p.Code
                })
                .ToList();
        }

        public IEnumerable<CityListingServiceModel> GetAllByCountry(int countryID)
        {
            return this.data.Cities
                .Where(c => c.CountryID == countryID)
                .Select(c => new CityListingServiceModel
                {
                    ID = c.ID,
                    Name = c.Name
                }).ToList();
        }
    }
}
