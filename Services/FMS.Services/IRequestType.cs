namespace FMS.Services
{
    public interface IRequestType
    {
        void Create(string name, string description);

        void Delete(int requestTypeID);
    }
}
