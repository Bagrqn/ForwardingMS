namespace FMS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string Bulstat { get; set; }

        [MaxLength(20)]
        public string TaxNUmber { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        [MaxLength(120)]
        public string Addres { get; set; }

        public int CompanyTypeId { get; set; }

        public CompanyType companyType { get; set; }
    }
}
