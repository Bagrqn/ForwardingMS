using FMS.Data.Models.Request;
using System;

namespace FMS.Services.Factory.Request
{
    public class RequestFactory
    {
        public static Data.Models.Request.Request Create(string number, int requestTypeID)
        {
            return new Data.Models.Request.Request()
            {
                Number = number,
                DateCreate = DateTime.UtcNow,
                IsDeleted = false,
                RequestTypeID = requestTypeID
            };
        }

        public static RequestNumericProp NewProp(string name, double value)
        {
            return new RequestNumericProp()
            {
                Name = name,
                Value = value
            };
        }
        public static RequestStringProps NewProp(string name, string value)
        {
            return new RequestStringProps()
            {
                Name = name,
                Value = value
            };
        }
    }
}
