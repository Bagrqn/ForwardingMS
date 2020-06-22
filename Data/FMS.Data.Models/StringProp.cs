using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models
{
    public class StringProp
    {
        public int Id { get; set; }

        public int PropNameId { get; set; }

        public PropName PropName { get; set; }

        public string Value { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
