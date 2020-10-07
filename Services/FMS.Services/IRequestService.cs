using FMS.Services.Models.Request;

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

        void AddEmployee(int requestID, int employeeID, int relationTypeID);

        RequestStatusServiceModel GetStatus(int requestID);

        RequestTypeServiceModel GetType(int requestID);

        RequestListingServiceModel GetRequest(string number);

        void NewCustomerRequest(FMS.Services.Models.Request.CurtomerRequestModel model);
    }
}
