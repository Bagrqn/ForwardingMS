using FMS.Services.Models.Request;

namespace FMS.Services
{
    public interface IRequest
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

    }
}
