using System.Collections.Generic;

namespace FMS.Data.Models
{
    public class Company
    {
        public int Id { get; set; }

        public ICollection<StringProp> StringProps { get; set; } = new HashSet<StringProp>();

        public ICollection<NumericProp> NumericProps { get; set; } = new HashSet<NumericProp>();

        public int CompanyTypeId { get; set; }

        public CompanyType companyType { get; set; }
    }
}
