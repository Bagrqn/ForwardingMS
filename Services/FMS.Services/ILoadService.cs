using FMS.Data.Models.Request;

namespace FMS.Services
{
    public interface ILoadService
    {
        void Create(int requestID, string name, string comment, int packageTypeID, int packageCount);

        void AddStringProp(string name, string value, int loadID);

        void AddNumericProp(string name, double value, int loadID);

        void AddLUP(int loadID, int lupID);
    }
}
