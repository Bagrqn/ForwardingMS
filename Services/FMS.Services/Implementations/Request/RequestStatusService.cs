using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestStatusService : IRequestStatus
    {
        private readonly FMSDBContext data;

        public RequestStatusService(FMSDBContext data)
            => this.data = data;
        public void Create(double code, string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Status code name can not be null. ");
            }
            if (name.Length > DataValidation.RequestStatusNameMaxLenght)
            {
                throw new InvalidOperationException($"Status code name can not be longer then {DataValidation.RequestStatusNameMaxLenght} charecters. ");
            }
            if (!data.RequestStatuses.Any(s => s.Code == 0))
            {
                CreateDefaultStatus();
            }
            if (data.RequestStatuses.Any(s => s.Code == code))
            {
                throw new InvalidOperationException($"This code: {code} alredy exist. ");
            }
            var status = new RequestStatus()
            {
                Code = code,
                Name = name,
                Description = description
            };
            data.RequestStatuses.Add(status);
            data.SaveChanges();
        }


        public int GetDefaultStatusID()
        {
            if (!data.RequestStatuses.Any(s => s.Code == 0))
            {
                CreateDefaultStatus();
            }
            return data.RequestStatuses.FirstOrDefault(s => s.Code == 0).ID;
        }

        public int GetStatusIDByCode(double code)
        {
            if (!data.RequestStatuses.Any(s => s.Code == code))
            {
                throw new InvalidOperationException($"Request status code {code} doesen't exist. ");
            }
            return data.RequestStatuses.FirstOrDefault(s => s.Code == code).ID;
        }
        private void CreateDefaultStatus()
        {
            data.RequestStatuses.Add(new RequestStatus()
            {
                Code = 0,
                Name = "Default",
                Description = "First status for every request."
            }); ;
            data.SaveChanges();
        }
    }
}
