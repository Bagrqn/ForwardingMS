using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models
{
    public class CompanyType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
