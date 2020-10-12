using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Data.Models.Request
{
    public class RequestNumericProp
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double Value { get; set; }

        public int RequestID { get; set; }

        public Request Request { get; set; }
    }
}
