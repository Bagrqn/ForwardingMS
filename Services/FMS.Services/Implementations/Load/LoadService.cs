using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using System;

namespace FMS.Services.Implementations.Load
{
    public class LoadService : ILoadService
    {
        private readonly FMSDBContext data;

        public LoadService(FMSDBContext data)
            => this.data = data;

        public void AddLUP(int loadID, int lupID)
        {
            var loadToLUP = new LoadToLUPoint()
            {
                LoadID = loadID,
                LoadingUnloadingPointID = lupID
            };
            data.LoadToLUPoints.Add(loadToLUP);
            data.SaveChanges();
        }

        public void AddNumericProp(string name, double value, int loadID)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of load numeric property can not be null or empty. ");
            }
            if (name.Length > DataValidation.PropetryNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of load numeric property can not be longer then {DataValidation.PropetryNameMaxLenght} characters. ");
            }
            var numericProp = new Data.Models.Request.LoadNumericProps()
            {
                Name = name,
                Value = value,
                LoadID = loadID
            };
            data.LoadNumericProps.Add(numericProp);
            data.SaveChanges();
        }

        public void AddStringProp(string name, string value, int loadID)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of load string property can not be null or empty. ");
            }
            if (name.Length > DataValidation.PropetryNameMaxLenght)
            {
                throw new InvalidOperationException($"Name of load string property can not be longer then {DataValidation.PropetryNameMaxLenght} characters. ");
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("Value of load string property can not be null or empty. ");
            }
            var strProp = new Data.Models.Request.LoadStringProp()
            {
                Name = name,
                Value = value,
                LoadID = loadID
            };

            data.LoadStringProps.Add(strProp);
            data.SaveChanges();
        }

        public void Create(int requestID, string name, string comment, int packageTypeID, int packageCount)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of load can not be null or empty. ");
            }
            if (name.Length > DataValidation.NameMaxLenght)
            {
                throw new InvalidOperationException($"Name of load can not be logner then {DataValidation.NameMaxLenght} characters. ");
            }

            var load = new FMS.Data.Models.Request.Load()
            {
                Name = name,
                Comment = comment,
                PackageTypeID = packageTypeID,
                PackageCount = packageCount,
                RequestID = requestID
            };

            data.Loads.Add(load);
            data.SaveChanges();
        }


    }
}
