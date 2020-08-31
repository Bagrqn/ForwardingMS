using System.Collections.Generic;
using FMS.Services.Models.Postcode;
namespace FMS.Services
{
    public interface IPostcodeService
    {
        void Create(string code);

        IEnumerable<PostcodeListingServiceModel> SearchByName(string name);
    }
}
