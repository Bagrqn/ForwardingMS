using FMS.Data.Models.Request;

namespace FMS.Services
{
    public interface ILUPService
    {
        void Create(int loadID, LoadingUnloadingPointTypeEnum lupType, int senderRecieverID, int cityID, int postcodeID, string Address, int requestID);
    }
}
