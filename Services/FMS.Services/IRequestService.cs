using FMS.Services.Models.Request;
using System.Collections.Generic;

namespace FMS.Services
{
    public interface IRequestService
    {
        void Create(string number, int requestTypeID);

        void NextStatus(int requestID);

        void Delete(int requestID);

        void AddAssignor(int requestID, int companyID);

        void AddSupplyer(int requestID, int companyID);

        void AddPayer(int requestID, int companyID);

        void AddCarrier(int requestID, int companyID);

        void AddEmployee(int requestID, int employeeID, int relationTypeID);

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
    }
}
