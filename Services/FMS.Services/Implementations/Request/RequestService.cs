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
            var status = data.Requests
                .Where(r => r.ID == requestID)
                .Select(r => r.RequestStatus)
                .FirstOrDefault();

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
            var type = data.Requests
                  .Where(r => r.ID == requestID)
                  .Select(r => r.RequestType)
                  .FirstOrDefault();

            return new RequestTypeServiceModel()
            {
                ID = type.ID,
                Name = type.Name,
                Description = type.Description
            };
        }

        public void AddAssignor(int requestID, int companyID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Assignor"))
            {
                AddRequestToCompanyRelationType("Assignor", "Възложител на заявката");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Assignor");
            data.RequestToCompanies.Add(new Data.Models.Request.RequestToCompany
            {
                RequestID = requestID,
                CompanyID = companyID,
                RequestToCompanyRelationTypeID = relationType.ID
            });
            data.SaveChanges();
        }

        public void AddSupplyer(int requestID, int companyID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Supplyer"))
            {
                AddRequestToCompanyRelationType("Supplyer", "Изпълнител на заявката");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Supplyer");
            data.RequestToCompanies.Add(new Data.Models.Request.RequestToCompany
            {
                RequestID = requestID,
                CompanyID = companyID,
                RequestToCompanyRelationTypeID = relationType.ID
            });
            data.SaveChanges();
        }

        public void AddPayer(int requestID, int companyID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Payer"))
            {
                AddRequestToCompanyRelationType("Payer", "Платец на заявката");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Payer");
            data.RequestToCompanies.Add(new Data.Models.Request.RequestToCompany
            {
                RequestID = requestID,
                CompanyID = companyID,
                RequestToCompanyRelationTypeID = relationType.ID
            });
            data.SaveChanges();
        }

        public void AddEmployee(int requestID, int employeeID, int relationTypeID)
        {
            data.RequestToEmployees.Add(new Data.Models.Request.RequestToEmployee()
            {
                RequestID = requestID,
                EmployeeID = employeeID,
                RequestToEmployeeRelationTypeID = relationTypeID
            });
        }

        private void AddRequestToCompanyRelationType(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Relation type name can not be null");
            }
            if (data.RequestToCompanyRelationTypes.Any(t => t.Name == name))
            {
                throw new InvalidOperationException($"Relation type {name} alredy exist. ");
            }
            data.RequestToCompanyRelationTypes.Add(new Data.Models.Request.RequestToCompanyRelationType()
            {
                Name = name,
                Description = description
            });
            data.SaveChanges();
        }

        private void AddRequestToEmployeeRelationType(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Relation type name can not be null");
            }
            if (data.RequestToEmployeeRelationTypes.Any(t => t.Name == name))
            {
                throw new InvalidOperationException($"Relation type {name} alredy exist. ");
            }
            data.RequestToEmployeeRelationTypes.Add(new Data.Models.Request.RequestToEmployeeRelationType()
            {
                Name = name,
                Description = description
            });
            data.SaveChanges();
        }

    }
}
