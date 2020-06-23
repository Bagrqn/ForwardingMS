namespace FMS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int CountryID { get; set; }

        public Country Country { get; set; }

        public ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
