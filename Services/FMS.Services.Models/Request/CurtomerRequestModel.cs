using FMS.Services.Models.Country;
using System.Collections;
using System.Collections.Generic;

namespace FMS.Services.Models.Request
{
    public class CurtomerRequestModel
    {
        //Request relations
        public int FromCountryID { get; set; }

        public int FromCityID { get; set; }

        public int FromPostcodeID { get; set; }

        public string FromAddress { get; set; }

        public int ToCountryID { get; set; }

        public int ToCityID { get; set; }

        public int ToPostcodeID { get; set; }

        public string ToAddress { get; set; }
        //Load main info
        public string LoadName { get; set; }

        public string LoadComment { get; set; } //Comment, short description, aditional info

        public int PackageTypeID { get; set; }

        public int PackageCount { get; set; }

        //Load props
        public double Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double WeightBrut { get; set; }

        public double WeightNet { get; set; }

        public double Lademeter { get; set; }

        public string StockType { get; set; }

        //Customer info
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }


        public IEnumerable<CountryListingServiceModel> countryList { get; set; }

    }
}
