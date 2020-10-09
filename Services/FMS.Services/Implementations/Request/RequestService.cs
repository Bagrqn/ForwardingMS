﻿using FMS.Data;
using FMS.Services.Factory;
using FMS.Services.Factory.Request;
using FMS.Services.Models.Request;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestService : IRequestService
    {
        private const int pageSize = 20;

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

        public RequestListingServiceModel GetRequest(string number)
        {
            var request = data.Requests.FirstOrDefault(r => r.Number == number);
            return new RequestListingServiceModel()
            {
                ID = request.ID,
                Number = request.Number,
                DateCreate = request.DateCreate,
                IsDeleted = request.IsDeleted
            };
        }

        public void NewCustomerRequest(CurtomerRequestModel model)
        {
            var requestTypeID = ServiceFactory.NewRequestTypeService(data).FindTypeByName("Transport").ID;
            var request = RequestFactory.Create(DateTime.UtcNow.ToString(), requestTypeID);
            request.RequestStatusID = ServiceFactory.NewRequestStatusService(data).GetDefaultStatusID();
            //Create load
            var load = LoadFactory.Create(model.LoadName, model.LoadComment, model.PackageTypeID, model.PackageCount);
            //Add props if have value
            if (model.Height != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("Height", model.Height));
            }
            if (model.Width != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("Width", model.Width));
            }
            if (model.WeightBrut != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("WeightBrut", model.WeightBrut));
            }
            if (model.WeightNet != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("WeightNet", model.WeightNet));
            }
            if (model.Lademeter != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("Lademeter", model.Lademeter));
            }
            if (!string.IsNullOrEmpty(model.StockType))
            {
                load.LoadStringProps.Add(LoadFactory.NewStringProp("StockType", model.StockType));
            }
            //Add Load to Request
            request.Loads.Add(load);
            //Sender/reciever missing reference / Hardcoded 20 - Not Defined Company (Test case)
            request.LoadingUnloadingPoints.Add(LUPFactory.NewLUP(Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading,
                                                                    model.FromCityID, model.FromPostcodeID, model.FromAddress, 20));
            //Sender/reciever missing reference / Hardcoded 20 - Not Defined Company (Test case)
            request.LoadingUnloadingPoints.Add(LUPFactory.NewLUP(Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading,
                                                                    model.ToCityID, model.ToPostcodeID, model.ToAddress, 20));



            data.Requests.Add(request);
            data.SaveChanges();
        }

        public IEnumerable<BasicRequestsLintingServiceModel> GetAllByStatus(int statusID, int page = 1)
        {
            var a = data.Requests
                .Where(r => r.RequestStatusID == statusID)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .Select(r => new BasicRequestsLintingServiceModel()
                {
                    ID = r.ID,
                    Number = r.Number,
                    DateCreate = r.DateCreate.ToString()
                }).ToList();
            foreach (var r in a)
            {
                r.FromCountryCity = GetFromCountry(r.ID).ToString() + " - " + GetFromCity(r.ID).ToString();
                r.ToCountryCity = GetToCountry(r.ID) + " - " + GetToCity(r.ID);
                r.LoadName = GetLoadName(r.ID);
            }

            return a;
        }


        public string GetFromCountry(int requestID)
        {
            var LUP = data.LoadingUnloadingPoints
                .Where(lup => lup.RequestID == requestID
                        && lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading)
                .FirstOrDefault();
            if (LUP == null)
            {
                return " ";
            }
            var a = data.Cities
                .Where(c => c.ID == LUP.CityID)
                .Select(c => c.Country.Name)
                .FirstOrDefault();
            ;
            return a;
        }

        public string GetFromCity(int requestID)
        {
            var cityName = data.LoadingUnloadingPoints
                .Where(lup => lup.RequestID == requestID
                        && lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading)
                .Select(lup => lup.City.Name)
                .FirstOrDefault();
            if (cityName == null)
            {
                return "";
            }
            ;
            return cityName;
        }

        public string GetToCountry(int requestID)
        {
            var LUP = data.LoadingUnloadingPoints
                .Where(lup => lup.RequestID == requestID
                        && lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading)
                .FirstOrDefault();
            if (LUP == null)
            {
                return "";
            }
            var a = data.Cities
                .Where(c => c.ID == LUP.CityID)
                .Select(c => c.Country.Name)
                .FirstOrDefault();
            ;
            return a;
        }

        public string GetToCity(int requestID)
        {
            var cityName = data.LoadingUnloadingPoints
                .Where(lup => lup.RequestID == requestID
                        && lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading)
                .Select(lup => lup.City.Name)
                .FirstOrDefault();
            ;
            return cityName;
        }

        public string GetLoadName(int requestID)
        {
            var load = data.Loads
                .Where(l => l.RequestID == requestID)
                .FirstOrDefault();
            if (load == null)
            {
                return "-";
            }
            return load.Name;
        }

        public int CountByStatus(int statusID)
        {
            return data.Requests
                .Where(r => r.RequestStatusID == statusID).Count();
        }
    }
}
