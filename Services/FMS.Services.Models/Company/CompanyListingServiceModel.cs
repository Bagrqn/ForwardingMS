using FMS.Data.Models.Company;
using System.Collections.Generic;

namespace FMS.Services.Models.Company
{
    public class CompanyListingServiceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Bulstat { get; set; }
        public string TaxNumber { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public int CompanyTypeID { get; set; }
        public List<CompanyStringProp> StringProps { get; set; } = new List<CompanyStringProp>();
        public List<CompanyNumericProp> NumericProps { get; set; } = new List<CompanyNumericProp>();
    }
}
