using FMS.Data;
using FMS.Data.Models;
using FMS.Services.Models.Country;
using System;
using System.Collections.Generic;

namespace FMS.Services.Implementations
{
    public class CountryService : ICountryService
    {
        private FMSDBContext data;
        public void Create(string name)
        {
            if (name.Length > DataValidation.CountryNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of country can not be longer then {DataValidation.CountryNameMaxLenght} characters. ");
            }

            var country = new Country
            {
                Name = name
            };
            this.data.Countries.Add(country);
            this.data.SaveChanges();
        }

        IEnumerable<CountryListingServiceModel> ICountryService.SearchByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
