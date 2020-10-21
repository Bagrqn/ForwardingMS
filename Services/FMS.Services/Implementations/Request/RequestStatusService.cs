using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using FMS.Services.Contracts;
using FMS.Services.Models.Request;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestStatusService : IRequestStatusService
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

        public RequestStatusServiceModel GetRequestStatus(int requestID)
        {
            var request = data.Requests.FirstOrDefault(r => r.ID == requestID);
            var status = data.RequestStatuses.FirstOrDefault(s => s.ID == request.RequestStatusID);
            return new RequestStatusServiceModel()
            {
                ID = status.ID,
                Code = status.Code,
                Name = status.Name,
                Description = status.Description
            };
        }

        //Get status refactored
        public RequestStatusServiceModel GetStatus(double code)
        {
            var status = data.RequestStatuses.FirstOrDefault(s => s.Code == code);

            if (code == CommonValues.RequestDefaultStatusCode && status == null)
            {
                Create(CommonValues.RequestDefaultStatusCode, CommonValues.RequestDefaultStatusName, CommonValues.RequestDefaultStatusDescription);
                status = data.RequestStatuses.FirstOrDefault(s => s.Code == code);
            }

            if (status == null)
            {
                throw new InvalidOperationException($"Request status code {code} doesen't exist. ");
            }

            return new RequestStatusServiceModel()
            {
                ID = status.ID,
                Code = status.Code,
                Name = status.Name,
                Description = status.Description
            };
        }
        public RequestStatusServiceModel GetStatus(string statusName)
        {
            var status = data.RequestStatuses.FirstOrDefault(s => s.Name == statusName);

            if (statusName == CommonValues.RequestDefaultStatusName && status == null)
            {
                Create(CommonValues.RequestDefaultStatusCode, CommonValues.RequestDefaultStatusName, CommonValues.RequestDefaultStatusDescription);
                status = data.RequestStatuses.FirstOrDefault(s => s.Name == statusName);
            }

            if (status == null)
            {
                throw new InvalidOperationException($"Request status name {statusName} doesen't exist. ");
            }

            return new RequestStatusServiceModel()
            {
                ID = status.ID,
                Code = status.Code,
                Name = status.Name,
                Description = status.Description
            };
        }
        public RequestStatusServiceModel GetStatus(int statusID)
        {
            var status = data.RequestStatuses.FirstOrDefault(s => s.ID == statusID);

            return new RequestStatusServiceModel()
            {
                ID = status.ID,
                Code = status.Code,
                Name = status.Name,
                Description = status.Description
            };
        }



        //Depricated
        /*
        public int GetDefaultStatusID()
        {
            if (!data.RequestStatuses.Any(s => s.Code == 0))
            {
                CreateDefaultStatus();
            }
            return data.RequestStatuses.FirstOrDefault(s => s.Code == 0).ID;
        }
        */
        public RequestStatus GetDeleteStatus()
        {
            var deleted = data.RequestStatuses.FirstOrDefault(s => s.Name == "Deleted");
            if (deleted == null)
            {
                data.RequestStatuses.Add(new RequestStatus()
                {
                    Name = "Deleted",
                    Code = 9,
                    Description = ""
                });
                data.SaveChanges();
                return data.RequestStatuses.FirstOrDefault(s => s.Name == "Deleted");
            }
            return deleted;
        }

        public RequestStatusServiceModel GetCustomsProcessingStatus()
        {
            var status = data.RequestStatuses.FirstOrDefault(s => s.Name == "CustomsProcessing");
            if (status == null)
            {
                double nextCode = Math.Ceiling(data.RequestStatuses.Select(r => r.Code).ToList().Max()) + 1;
                Create(nextCode, "CustomsProcessing", "");
                status = data.RequestStatuses.FirstOrDefault(s => s.Name == "CustomsProcessing");
                return new RequestStatusServiceModel()
                {
                    Code = status.Code,
                    ID = status.ID,
                    Name = status.Name,
                    Description = status.Description
                };
            }
            return new RequestStatusServiceModel()
            {
                Code = status.Code,
                ID = status.ID,
                Name = status.Name,
                Description = status.Description
            };
        }

        //Depricated
        /*public int GetStatusIDByCode(double code)
        {
            if (!data.RequestStatuses.Any(s => s.Code == code))
            {
                throw new InvalidOperationException($"Request status code {code} doesen't exist. ");
            }
            return data.RequestStatuses.FirstOrDefault(s => s.Code == code).ID;
        }*/

        public void LogStatusCganhe(int requestID, int oldStatusID, int newStatusID)
        {
            throw new NotImplementedException();
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
