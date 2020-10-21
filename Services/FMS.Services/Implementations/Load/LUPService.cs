using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using FMS.Services.Contracts;
using System;

namespace FMS.Services.Implementations.Load
{
    public class LUPService : ILUPService
    {
        private readonly FMSDBContext data;

        public LUPService(FMSDBContext data)
            => this.data = data;

        public void Create(int loadID, LoadingUnloadingPointTypeEnum lupType, int senderRecieverID, int cityID, int postcodeID, string address, int requestID)
        {
            if (string.IsNullOrEmpty(address))
            {
                throw new InvalidOperationException("LUP address can not be null or empty. ");
            }
            if (address.Length > DataValidation.AddressMaxLenght)
            {
                throw new InvalidOperationException($"LUP address can not be longer then {DataValidation.AddressMaxLenght} characters. ");
            }

            var lup = new LoadingUnloadingPoint();
            lup.Type = lupType;
            lup.SenderRecieverID = senderRecieverID;
            lup.CityID = cityID;
            lup.PostcodeID = postcodeID;
            lup.Address = address;
            lup.RequestID = requestID;
            lup.Date = DateTime.UtcNow;

            var loadToLUP = new LoadToLUPoint();
            loadToLUP.LoadID = loadID;
            loadToLUP.LoadingUnloadingPoint = lup;

            data.LoadingUnloadingPoints.Add(lup);
            data.LoadToLUPoints.Add(loadToLUP);

            data.SaveChanges();
        }
    }
}
