using FMS.Services.Models.Request;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface ILoadService
    {
        void Create(int requestID, string name, string comment, int packageTypeID, int packageCount);

        void AddStringProp(string name, string value, int loadID);

        void AddNumericProp(string name, double value, int loadID);

        void AddLUP(int loadID, int lupID);

        void CreatePackageType(string name);

        IEnumerable<PackageTypeListingServiceModel> GetAllTypes();
    }
}
