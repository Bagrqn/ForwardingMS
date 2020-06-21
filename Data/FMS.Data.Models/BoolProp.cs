using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models
{
    public class BoolProp
    {
        public int Id { get; set; }

        public int PropNameID { get; set; }
        
        public PropName PropName { get; set; }

        public bool Value { get; set; }
    }
}
