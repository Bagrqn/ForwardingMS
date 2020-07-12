using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Company
{
    public class Company
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.CompanyNameMaxLenght)]
        public string Name { get; set; }

        [MaxLength(DataValidation.CompanyBulstatMaxLenght)]
        public string Bulstat { get; set; }

        [MaxLength(DataValidation.CompanyTaxNumberMaxLenght)]
        public string TaxNumber { get; set; }

        public int CountryID { get; set; }

        public Country Country { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        [MaxLength(DataValidation.CompanyAddresMaxLenght)]
        public string Addres { get; set; }

        public int CompanyTypeID { get; set; }

        public CompanyType CompanyType { get; set; }

        public ICollection<CompanyStringProp> CompanyStringProps { get; set; } = new HashSet<CompanyStringProp>();

        public ICollection<CompanyNumericProp> CompanyNumericProps { get; set; } = new HashSet<CompanyNumericProp>();

        public ICollection<CompanyBoolProp> CompanyBoolProps { get; set; } = new HashSet<CompanyBoolProp>();

        public ICollection<Request.RequestToCompany> RequestToCompanies { get; set; } = new HashSet<Request.RequestToCompany>();

        public ICollection<Request.LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; } = new HashSet<Request.LoadingUnloadingPoint>();
    }
}
