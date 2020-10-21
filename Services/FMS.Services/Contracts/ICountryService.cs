using FMS.Services.Models.Country;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface ICountryService
    {
        void Create(string name);

        IEnumerable<CountryListingServiceModel> SearchByName(string name);

        IEnumerable<CountryListingServiceModel> GetAll();
    }
}
