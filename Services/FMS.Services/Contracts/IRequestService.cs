using FMS.Services.Models.Request;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface IRequestService
    {
        void Create(string number, int requestTypeID);

        void ProcessToNextStatus(int requestID);

        int GetNextStatus(int requestID);

        void Delete(int requestID);

        void AddAssignor(int requestID, int companyID);

        void AddSupplyer(int requestID, int companyID);

        void AddPayer(int requestID, int companyID);

        void AddCarrier(int requestID, int companyID);

        void AddEmployee(int requestID, int employeeID, int relationTypeID);

        void AddStringProp(int requestID, string name, string value);

        void AddNumericProp(int requestID, string name, double value);

        RequestStatusServiceModel GetStatus(int requestID);

        RequestTypeServiceModel GetType(int requestID);

        RequestListingServiceModel GetRequest(string number);
        FullInfoRequestServiceModel GetRequest(int requestId);

        IEnumerable<BasicRequestsLintingServiceModel> GetAllByStatus(int statusID, int page = 1);

        void NewCustomerRequest(FMS.Services.Models.Request.CurtomerRequestModel model);

        string GetFromCountry(int requestID);

        string GetFromCity(int requestID);

        string GetToCountry(int requestID);

        string GetToCity(int requestID);

        string GetLoadName(int requestID);

        int CountByStatus(int statusID);

        void SaveChanges(FullInfoRequestServiceModel model);

        void ProcessToPayed(int reqiestID);
    }
}
