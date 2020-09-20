namespace FMS.Services
{
    public interface IRequest
    {
        void Create();

        void NextStatus();

        void Delete();
    }
}
