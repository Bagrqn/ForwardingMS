using FMS.Data.Models.Request;
using FMS.Services.Implementations.Request;
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
    }
}
