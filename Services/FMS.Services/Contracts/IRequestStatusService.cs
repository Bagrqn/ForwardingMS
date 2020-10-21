using FMS.Data.Models.Request;
using FMS.Services.Models.Request;

namespace FMS.Services.Contracts
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

        RequestStatusServiceModel GetCustomsProcessingStatus();

        RequestStatusServiceModel GetRequestStatus(int requestID);

        RequestStatusServiceModel GetStatus(double code);
        
        RequestStatusServiceModel GetStatus(int statusID);


        void LogStatusCganhe(int requestID, int oldStatusID, int newStatusID);
    }
}
