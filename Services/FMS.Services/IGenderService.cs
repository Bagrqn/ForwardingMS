using FMS.Data.Models.Employee;

namespace FMS.Services
{
    public interface IGenderService
    {
        void Create(string name);
        Gender GetDefaultGender();
    }
}
