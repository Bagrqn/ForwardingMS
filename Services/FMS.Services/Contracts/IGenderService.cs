using FMS.Data.Models.Employee;

namespace FMS.Services.Contracts
{
    public interface IGenderService
    {
        void Create(string name);
        Gender GetDefaultGender();
    }
}
