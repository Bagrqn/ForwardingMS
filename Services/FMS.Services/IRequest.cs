namespace FMS.Services
{
    public interface IRequest
    {
        void Create(string number, int requestTypeID);

        void NextStatus();

        void Delete();
    }
}
