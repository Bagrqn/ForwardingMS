using FMS.Services.Models.City;
using FMS.Services.Models.Postcode;
using System.Collections.Generic;

namespace FMS.Services
{
    public interface ICityService
    {
        void Create(string name, int countryID);

        IEnumerable<CityListingServiceModel> SearchByName(string name);
        ICollection<int> GetAllIDsByCountry(int countryID);
        IEnumerable<PostcodeListingServiceModel> GetPostcodes(int cityID);
        IEnumerable<CityListingServiceModel> GetAllByCountry(int countryID);
    }
}
