using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models
{
    public class NumericProp
    {
        public int Id { get; set; }

        public int PropNameID { get; set; }

        public PropName propName { get; set; }

        public double Value { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
