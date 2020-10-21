using System.Collections.Generic;
using FMS.Services.Models.Postcode;

namespace FMS.Services.Contracts
{
    public interface IPostcodeService
    {
        void Create(string code, int cityID);

        IEnumerable<PostcodeListingServiceModel> SearchByName(string name);
    }
}
