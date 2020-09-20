using FMS.Data.Models.Request;
using System;

namespace FMS.Services.Implementations.Request
{
    public class RequestService : IRequest
    {
        public void Create(string number, int requestTypeID)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new InvalidOperationException("Request number can not be null. ");
            }
            var request = new Data.Models.Request.Request()
            {
                Number = number,
                DateCreate = DateTime.UtcNow,
                IsDeleted = false,
                RequestTypeID = requestTypeID,
                RequestStatusID = 1
            };
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void NextStatus()
        {
            throw new NotImplementedException();
        }
    }
}
