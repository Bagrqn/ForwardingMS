using FMS.Data;
using FMS.Data.Models;
using FMS.Services.Models.Postcode;
using System;
using System.Collections.Generic;

namespace FMS.Services.Implementations
{
    public class PostcodeService : IPostcodeService
    {
        private readonly FMSDBContext data;

        public void Create(string code)
        {
            if (code.Length > DataValidation.PostcodeMaxLenght)
            {
                throw new ArgumentException($"Postcode can not be longer then {DataValidation.PostcodeMaxLenght} characters.");
            }
            //to do insert in db with City 
        }

        public IEnumerable<PostcodeListingServiceModel> SearchByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
