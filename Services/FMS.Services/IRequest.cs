using FMS.Services.Models.Request;

namespace FMS.Services
{
    public interface IRequest
    {
        void Create(string number, int requestTypeID);

        void NextStatus(int requestID);

        void Delete(int requestID);

        RequestStatusServiceModel GetStatus(int requestID);

        RequestTypeServiceModel GetType(int requestID);
    }
}
