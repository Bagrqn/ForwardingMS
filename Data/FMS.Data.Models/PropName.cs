using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models
{
    public class PropName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BoolProp> BoolProps { get; set; } = new HashSet<BoolProp>();

        public ICollection<NumericProp> NumericProps { get; set; } = new HashSet<NumericProp>();

        public ICollection<StringProp> StringProps { get; set; } = new HashSet<StringProp>();

        public string StringPropertyFromBZHome { get; set; }
    }
}
