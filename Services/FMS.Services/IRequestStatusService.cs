using FMS.Services.Models.Request;

namespace FMS.Services
{
    public interface IRequestStatusService
    {
        void Create(double code, string name, string description);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> Status code ID </returns>
        int GetDefaultStatusID();

        int GetStatusIDByCode(double code);

        RequestStatusServiceModel GetRequestStatus(int requestID);

        RequestStatusServiceModel GetStatus(double code);
        
        void LogStatusCganhe(int requestID, int oldStatusID, int newStatusID);
    }
}
