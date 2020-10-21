using FMS.Services.Models.Request;

namespace FMS.Services.Contracts
{
    public interface IRequestStatusService
    {
        void Create(double code, string name, string description);

        //Depricated
        //int GetStatusIDByCode(double code);

        RequestStatusServiceModel GetCustomsProcessingStatus();

        RequestStatusServiceModel GetRequestStatus(int requestID);

        RequestStatusServiceModel GetStatus(double code);

        RequestStatusServiceModel GetStatus(string statusName);

        RequestStatusServiceModel GetStatus(int statusID);


        void LogStatusCganhe(int requestID, int oldStatusID, int newStatusID);
    }
}
