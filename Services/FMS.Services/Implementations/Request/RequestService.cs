using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
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
            //set status to deleted!! 
            int delStatID = new RequestStatusService(data).GetDeleteStatus().ID;
            var currentStatus = new RequestStatusService(data).GetRequestStatus(requestID);
            //Log event in status history
            data.RequestStatusHistories.Add(new Data.Models.Request.RequestStatusHistory
            {
                RequestID = requestID,
                NewStatusID = delStatID,
                OldStatusID = currentStatus.ID,
                DateChange = DateTime.UtcNow
            });

            //Change status. 
            request.RequestStatusID = delStatID;
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

        public void AddCarrier(int requestID, int companyID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Carrier"))
            {
                AddRequestToCompanyRelationType("Carrier", "Превозвач");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Carrier");

            var relation = data.RequestToCompanies.FirstOrDefault(x => x.RequestID == requestID
                                        && x.RequestToCompanyRelationTypeID == relationType.ID);
            if (relation != null)
            {
                //update
                data.RequestToCompanies.Remove(relation);

            }

            data.RequestToCompanies.Add(new RequestToCompany
            {
                RequestID = requestID,
                CompanyID = companyID,
                RequestToCompanyRelationTypeID = relationType.ID
            });


            data.SaveChanges();
        }

        public void AddSupplyer(int requestID, int companyID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Supplier"))
            {
                AddRequestToCompanyRelationType("Supplier", "Доставчин на услугата.");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Supplier");
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

            //If relation alredy exist.
            var existingrelation = data.RequestToCompanies.FirstOrDefault(r => r.RequestID == requestID && r.CompanyID == companyID && r.RequestToCompanyRelationTypeID == relationType.ID);
            if (existingrelation != null)
            {
                return;
            }
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

        public FullInfoRequestServiceModel GetRequest(int requestId)
        {
            //Request
            var request = data.Requests.FirstOrDefault(r => r.ID == requestId);
            //Loads
            var load = data.Loads
                .Where(l => l.RequestID == requestId)
                .Select(l => new
                {
                    Name = l.Name,
                    l.PackageCount,
                    TypeName = l.PackageType.TypeName,
                    TypeID = l.PackageType.ID,
                    l.Comment,
                    Lenght = l.LoadNumericProps.FirstOrDefault(x => x.Name == "Length").Value,
                    Width = l.LoadNumericProps.FirstOrDefault(x => x.Name == "Width").Value,
                    Height = l.LoadNumericProps.FirstOrDefault(x => x.Name == "Height").Value,
                    WeightBrut = l.LoadNumericProps.FirstOrDefault(x => x.Name == "WeightBrut").Value,
                    WeightNet = l.LoadNumericProps.FirstOrDefault(x => x.Name == "WeightNet").Value,
                    Lademeter = l.LoadNumericProps.FirstOrDefault(x => x.Name == "Lademeter").Value,
                    StockType = l.LoadStringProps.FirstOrDefault(x => x.Name == "StockType").Value
                })
                .FirstOrDefault();
            //LUPs info From-To
            var LUPFrom = data.LoadingUnloadingPoints
                .Where(lup => lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading && lup.RequestID == requestId)
                .Select(lup => new
                {
                    ID = lup.ID,
                    PostcodeCode = lup.Postcode.Code,
                    Address = lup.Address,
                    CityName = lup.City.Name,
                    CountryName = lup.City.Country.Name
                })
                .FirstOrDefault();
            string fromInfo = "";
            if (LUPFrom != null)
            {
                fromInfo = $"{LUPFrom.CountryName} - {LUPFrom.CityName} - {LUPFrom.Address} ; Postcode: {LUPFrom.PostcodeCode}";
            }
            var LUPTo = data.LoadingUnloadingPoints
               .Where(lup => lup.Type == Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading && lup.RequestID == requestId)
               .Select(lup => new
               {
                   ID = lup.ID,
                   PostcodeCode = lup.Postcode.Code,
                   Address = lup.Address,
                   CityName = lup.City.Name,
                   CountryName = lup.City.Country.Name
               })
               .FirstOrDefault();
            string toInfo = "";
            if (LUPTo != null)
            {
                toInfo = $"{LUPTo.CountryName} - {LUPTo.CityName} - {LUPTo.Address} ; Postcode: {LUPTo.PostcodeCode}";
            }


            //Assignor info
            var assignor = data.RequestToEmployees
                .Where(x => x.RequestID == requestId
                    && x.RequestToEmployeeRelationTypeID == GetRequestToEmployeeRelationTypeAssignor().ID)
                .Select(x => new
                {
                    AssignorFirstName = x.Employee.FirstName,
                    AssignorLastName = x.Employee.LastName,
                    AssignorPhoneNumber = x.Employee.EmployeeStringProps.FirstOrDefault(p => p.Name == "Phone number").Value,
                    AssignorCompanyName = x.Employee.EmployeeStringProps.FirstOrDefault(p => p.Name == "Company name").Value,
                }).FirstOrDefault();
            ;
            if (request == null)
            {
                return null;
            }
            var fullRequestInfo = new FullInfoRequestServiceModel
            {
                ID = requestId,
                Number = request.Number,
                DateCreate = request.DateCreate.ToString(),
                FromInfo = fromInfo,
                ToInfo = toInfo,

            };
            if (assignor != null)
            {
                fullRequestInfo.AssignorFirstName = assignor.AssignorFirstName;
                fullRequestInfo.AssignorLastName = assignor.AssignorLastName;
                fullRequestInfo.AssignorPhoneNumber = assignor.AssignorPhoneNumber;
                fullRequestInfo.AssignorCompanyName = assignor.AssignorCompanyName;

            }
            if (load != null)
            {
                fullRequestInfo.Length = load.Lenght;
                fullRequestInfo.Width = load.Width;
                fullRequestInfo.Height = load.Height;
                fullRequestInfo.WeightBrut = load.WeightBrut;
                fullRequestInfo.WeightNet = load.WeightNet;
                fullRequestInfo.Lademeter = load.Lademeter;
                fullRequestInfo.StockType = load.StockType;
                fullRequestInfo.LoadName = load.Name;
                fullRequestInfo.LoadComment = load.Comment;
                fullRequestInfo.PackageCount = load.PackageCount;
                fullRequestInfo.PackageTypeID = load.TypeID;

            }
            ;
            //Chech carrier company
            var carrierCompany = data.RequestToCompanies.FirstOrDefault(rtc => rtc.RequestID == requestId
                                                                && rtc.RequestToCompanyRelationTypeID == Factory.ServiceFactory.NewCompanyService(data).GetTypeCarrierID()
                                                                );
            if (carrierCompany != null)
            {
                fullRequestInfo.CarrierConpanyID = carrierCompany.CompanyID;
            }
            //Chech payer company (Client)
            var payerCompany = data.RequestToCompanies.FirstOrDefault(rtc => rtc.RequestID == requestId
                                                                && rtc.RequestToCompanyRelationTypeID == ServiceFactory.NewCompanyService(data).GetCompanyTypeID("Client"));
            if (payerCompany != null)
            {
                fullRequestInfo.PayerConpanyID = payerCompany.CompanyID;
            }

            //Loading prices from db if exist. Carrier price / Client price / Saldo
            var carrierPriceProp = data.RequestNumericProps.FirstOrDefault(p => p.Name == "CarrierPrice" && p.RequestID == requestId);
            if (carrierCompany != null)
            {
                fullRequestInfo.CarrierPrice = carrierPriceProp.Value;
            }

            var clientPriceProp = data.RequestNumericProps.FirstOrDefault(p => p.Name == "ClientPrice" && p.RequestID == requestId);
            if (clientPriceProp != null)
            {
                fullRequestInfo.ClientPrice = clientPriceProp.Value;
            }

            var saldoProp = data.RequestNumericProps.FirstOrDefault(p => p.Name == "Saldo" && p.RequestID == requestId);
            if (saldoProp != null)
            {
                fullRequestInfo.Saldo = saldoProp.Value;
            }

            return fullRequestInfo;
        }

        public void NewCustomerRequest(CurtomerRequestModel model)
        {
            var requestTypeID = ServiceFactory.NewRequestTypeService(data).FindTypeByName("Transport").ID;
            var request = RequestFactory.Create(DateTime.UtcNow.ToString(), requestTypeID);
            request.RequestStatusID = ServiceFactory.NewRequestStatusService(data).GetDefaultStatusID();
            //Create load
            var load = LoadFactory.Create(model.LoadName, model.LoadComment, model.PackageTypeID, model.PackageCount);
            //Add props if have value Length
            if (model.Length != 0)
            {
                load.LoadNumericProps.Add(LoadFactory.NewNumericProps("Length", model.Length));
            }
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
                                                                    model.FromCityID, model.FromPostcodeID, model.FromAddress,
                                                                    ServiceFactory.NewCompanyService(data).GetUndefined().ID));
            //Sender/reciever missing reference / Hardcoded 20 - Not Defined Company (Test case)
            request.LoadingUnloadingPoints.Add(LUPFactory.NewLUP(Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading,
                                                                    model.ToCityID, model.ToPostcodeID, model.ToAddress,
                                                                    ServiceFactory.NewCompanyService(data).GetUndefined().ID));

            //Add assignor info.
            var employeeAssignor = Factory.Employee.EmployeeFactory.NewEmployee();
            employeeAssignor.GenderID = new GenderService(data).GetDefaultGender().ID;
            employeeAssignor.FirstName = model.FirstName;
            employeeAssignor.LastName = model.LastName;
            employeeAssignor.MiddleName = "-";
            employeeAssignor.EGN = "-";
            employeeAssignor.BirthDate = DateTime.MinValue;

            request.RequestToEmployees.Add(new Data.Models.Request.RequestToEmployee()
            {
                Employee = employeeAssignor,
                Request = request,
                RequestToEmployeeRelationType = GetRequestToEmployeeRelationTypeAssignor()
            });
            employeeAssignor.EmployeeStringProps.Add(new Data.Models.Employee.EmployeeStringProp()
            {
                Name = "Phone number",
                Value = model.PhoneNumber
            });
            employeeAssignor.EmployeeStringProps.Add(new Data.Models.Employee.EmployeeStringProp()
            {
                Name = "Company name",
                Value = model.CompanyName
            });

            ;//Компания за сега няма да се добавя! 
            data.Requests.Add(request);
            data.SaveChanges();
        }

        public RequestToEmployeeRelationType GetRequestToEmployeeRelationTypeAssignor()
        {
            var hasAssignor = data.RequestToEmployeeRelationTypes.Any(t => t.Name == "Assignor");
            if (!hasAssignor)
            {
                data.RequestToEmployeeRelationTypes.Add(new Data.Models.Request.RequestToEmployeeRelationType
                {
                    Name = "Assignor",
                    Description = "This person is creator of request. "
                });
                data.SaveChanges();
            }
            return data.RequestToEmployeeRelationTypes.FirstOrDefault(t => t.Name == "Assignor");
        }

        public IEnumerable<BasicRequestsLintingServiceModel> GetAllByStatus(int statusID, int page = 1)
        {
            var a = data.Requests
                .Where(r => r.RequestStatusID == statusID)
                .Skip((page - 1) * pageSize)
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

        public void AddStringProp(int requestID, string name, string value)
        {
            var existingProp = data.RequestStringProps.FirstOrDefault(p => p.RequestID == requestID && p.Name == name);
            if (existingProp == null)
            {
                data.RequestStringProps.Add(new RequestStringProps()
                {
                    Name = name,
                    Value = value,
                    RequestID = requestID
                });
                data.SaveChanges();
                return;
            }
            existingProp.Value = value;
            data.SaveChanges();
        }
        public void AddNumericProp(int requestID, string name, double value)
        {
            var existingProp = data.RequestNumericProps.FirstOrDefault(p => p.RequestID == requestID && p.Name == name);
            if (existingProp == null)
            {
                data.RequestNumericProps.Add(new RequestNumericProp()
                {
                    Name = name,
                    Value = value,
                    RequestID = requestID
                });
                data.SaveChanges();
                return;
            }
            existingProp.Value = value;
            data.SaveChanges();
        }

        public void SaveChanges(FullInfoRequestServiceModel model)
        {
            var load = data.Loads.Where(l => l.RequestID == model.ID).FirstOrDefault();

            load.Comment = model.LoadComment;
            load.PackageCount = model.PackageCount;
            load.PackageTypeID = model.PackageTypeID;
            LoadNumericPropertyUpdate("Length", model.Length, load.ID);
            LoadNumericPropertyUpdate("Height", model.Height, load.ID);
            LoadNumericPropertyUpdate("Width", model.Width, load.ID);
            LoadNumericPropertyUpdate("WeightBrut", model.WeightBrut, load.ID);
            LoadNumericPropertyUpdate("WeightNet", model.WeightNet, load.ID);
            LoadNumericPropertyUpdate("Lademeter", model.Lademeter, load.ID);
            if (!string.IsNullOrEmpty(model.StockType))
            {
                LoadStringPropertyUpdate("StockType", model.StockType, load.ID);
            }

            if (model.CarrierConpanyID != 0)
            {
                AddCarrier(model.ID, model.CarrierConpanyID);
            }
            if (model.PayerConpanyID != 0)
            {
                AddPayer(model.ID, model.PayerConpanyID);
            }
            //Add supplier
            AddMainCompany(model.ID);


            //REQUEST NUMERIC PROPS!!!!!!! Service!! 
            AddNumericProp(model.ID, "ClientPrice", model.ClientPrice);
            AddNumericProp(model.ID, "CarrierPrice", model.CarrierPrice);
            AddNumericProp(model.ID, "Saldo", model.Saldo);


            data.SaveChanges();
        }

        private void AddMainCompany(int requestID)
        {
            if (!data.RequestToCompanyRelationTypes.Any(t => t.Name == "Main"))
            {
                AddRequestToCompanyRelationType("Main", "Основна компания.");
            }
            var relationType = data.RequestToCompanyRelationTypes.FirstOrDefault(t => t.Name == "Main");
            var rel = data.RequestToCompanies.FirstOrDefault(x => x.RequestID == requestID && x.RequestToCompanyRelationTypeID == relationType.ID);
            if (rel == null)
            {
                data.RequestToCompanies.Add(new Data.Models.Request.RequestToCompany
                {
                    RequestID = requestID,
                    //To do... which is the main company
                    CompanyID = data.Companies.FirstOrDefault(c => c.CompanyTypeID == (data.CompanyTypes.FirstOrDefault(ct => ct.Name == "Main").ID)).ID,
                    RequestToCompanyRelationTypeID = relationType.ID
                });
                data.SaveChanges();
            }
        }

        private void LoadStringPropertyUpdate(string name, string value, int loadID)
        {
            bool hasProp = data.LoadStringProps.Any(p => p.Name == name && p.LoadID == loadID);
            if (!hasProp)
            {
                ServiceFactory.NewLoadService(data).AddStringProp(name, value, loadID);
            }
            var prop = data.LoadStringProps.FirstOrDefault(p => p.LoadID == loadID && p.Name == name);
            prop.Value = value;
            data.SaveChanges();
        }

        private void LoadNumericPropertyUpdate(string name, double value, int loadID)
        {
            bool hasProp = data.LoadNumericProps.Any(p => p.Name == name && p.LoadID == loadID);
            if (!hasProp)
            {
                ServiceFactory.NewLoadService(data).AddNumericProp(name, value, loadID);
            }
            var prop = data.LoadNumericProps.FirstOrDefault(p => p.LoadID == loadID && p.Name == name);
            prop.Value = value;
            data.SaveChanges();
        }
    }
}
