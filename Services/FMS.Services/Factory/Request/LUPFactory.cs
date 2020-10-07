using FMS.Data.Models.Request;

namespace FMS.Services.Factory.Request
{
    public class LUPFactory
    {
        public static Data.Models.Request.LoadingUnloadingPoint NewLUP(LoadingUnloadingPointTypeEnum lupType, int cityID, int postcodeID, string address, int senderReceiverId)
        {
            return new Data.Models.Request.LoadingUnloadingPoint()
            {
                Type = lupType,
                CityID = cityID,
                PostcodeID = postcodeID,
                Address = address,
                SenderRecieverID = senderReceiverId
            };
        }
    }
}
