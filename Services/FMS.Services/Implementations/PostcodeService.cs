using FMS.Data;
using FMS.Data.Models;
using FMS.Services.Models.Postcode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class PostcodeService : IPostcodeService
    {
        private readonly FMSDBContext data;

        public PostcodeService(FMSDBContext data)
            => this.data = data;

        public void Create(string code, int cityID)
        {
            if (code.Length > DataValidation.PostcodeMaxLenght)
            {
                throw new InvalidOperationException($"Postcode can not be longer then {DataValidation.PostcodeMaxLenght} characters.");
            }
            var postcode = new Postcode()
            {
                Code = code,
                CityID = cityID
            };
            data.Postcodes.Add(postcode);
            data.SaveChanges();
        }

        public IEnumerable<PostcodeListingServiceModel> SearchByName(string name)
        {
            return data.Postcodes
                .Where(p => p.Code == name)
                .Select(p => new PostcodeListingServiceModel()
                {
                    ID = p.ID,
                    Code = p.Code
                });
        }
    }
}
