using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestTypeService : IRequestType
    {
        private readonly FMSDBContext data;

        public RequestTypeService(FMSDBContext data)
            => this.data = data;
        public void Create(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Request type name can not be null or empty. ");
            }
            if (name.Length > DataValidation.RequestTypeNameMaxLenght)
            {
                throw new InvalidOperationException($"Request type name can not be longer then {DataValidation.RequestTypeNameMaxLenght} characters. ");
            }
            if (data.RequestTypes.Any(rt => rt.Name.ToLower() == name.ToLower()))
            {
                throw new InvalidOperationException($"Request type: {name}, already exist. ");
            }
            var requestType = new RequestType
            {
                Name = name,
                Description = description,
                IsDeleted = false
            };

            data.RequestTypes.Add(requestType);
            data.SaveChanges();
        }

        public void Delete(int requestTypeID)
        {
            var requestType = data.RequestTypes.FirstOrDefault(r => r.ID == requestTypeID);
            requestType.IsDeleted = true;
            data.SaveChanges();
        }
    }
}
