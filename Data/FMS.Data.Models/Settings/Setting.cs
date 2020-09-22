using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Settings
{
    public class Setting
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public string Description { get; set; }
    }
}
