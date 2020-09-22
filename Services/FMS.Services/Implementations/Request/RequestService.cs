using FMS.Data;
using FMS.Services.Models.Request;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestService : IRequest
    {
        private readonly FMSDBContext data;

        public RequestService(FMSDBContext data)
            => this.data = data;
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
                RequestStatusID = new RequestStatusService(data).GetDefaultStatusID()
            };
            data.Requests.Add(request);
            data.SaveChanges();
        }

        public void Delete(int requestID)
        {
            var request = data.Requests.FirstOrDefault(r => r.ID == requestID);
            request.IsDeleted = true;
            data.SaveChanges();
        }

        public void NextStatus(int requestID)
        {

            // Според типа на request-a чете в файл и гледа кой мy е следващия статус. 
            // През този файл се настройва процеса. 
            // Като се каже NextStatus, трябва да пише в Status histori да има история кво е станало.

            var requestType = GetType(requestID);

            //Read setting from file for this request type.
            string filePath = new SettingService(data).GetSetting("RequestStatusSettingFilePath");
            string fileText = File.ReadAllText(filePath);
            var json = JObject.Parse(fileText);

            var requestTypeObject = json["RequestTypes"].Children().Where(t => t["RequestType"].ToString() == requestType.ID.ToString()).First();
            var statusCodeSequence = requestTypeObject["StatusSequence"].ToList().Select(s => s.ToString()).ToList();

            //Find next status ID
            var statusService = new RequestStatusService(data);
            var currentstatus = statusService.GetRequestStatus(requestID);

            int currentStatusListIndexPosition = statusCodeSequence.FindIndex(e => e == currentstatus.Code.ToString());
            if (statusCodeSequence.Count - 1 == currentStatusListIndexPosition)
            {
                throw new InvalidOperationException("This is the last status from StatusSequence. ");
            }

            int nextStatusListIndexPosition = currentStatusListIndexPosition + 1;
            double nextStatusCode = double.Parse(statusCodeSequence[nextStatusListIndexPosition]);

            var nextStatus = statusService.GetStatus(nextStatusCode);
            ;
            //Change status. 
            var request = data.Requests.FirstOrDefault(r => r.ID == requestID);
            request.RequestStatusID = nextStatus.ID;

            //Log event in status history
            data.RequestStatusHistories.Add(new Data.Models.Request.RequestStatusHistory 
            {
                RequestID = requestID,
                NewStatusID = nextStatus.ID,
                OldStatusID = currentstatus.ID,
                DateChange = DateTime.UtcNow
            });

            data.SaveChanges();
        }


        public RequestStatusServiceModel GetStatus(int requestID)
        {
            int requestStatusID = data.Requests.FirstOrDefault(r => r.ID == requestID).RequestStatusID;
            var status = data.RequestStatuses.FirstOrDefault(rs => rs.ID == requestStatusID);

            return new RequestStatusServiceModel()
            {
                ID = status.ID,
                Code = status.Code,
                Name = status.Name,
                Description = status.Description
            };
        }

        public RequestTypeServiceModel GetType(int requestID)
        {
            int typeID = data.Requests.FirstOrDefault(r => r.ID == requestID).RequestTypeID;
            var type = data.RequestTypes.FirstOrDefault(t => t.ID == typeID);

            return new RequestTypeServiceModel()
            {
                ID = type.ID,
                Name = type.Name,
                Description = type.Description
            };
        }
    }
}
