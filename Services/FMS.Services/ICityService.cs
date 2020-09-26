using FMS.Services.Models.City;
using System.Collections.Generic;

namespace FMS.Services
{
    public interface ICityService
    {
        void Create(string name, int countryID);

        IEnumerable<CityListingServiceModel> SearchByName(string name);
    }
}
