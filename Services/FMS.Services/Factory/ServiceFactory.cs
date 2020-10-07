using FMS.Data;

namespace FMS.Services.Factory
{
    public class ServiceFactory
    {
        public static Implementations.Request.RequestTypeService NewRequestTypeService(FMSDBContext data)
        {
            return new Implementations.Request.RequestTypeService(data);
        }

        public static Implementations.Request.RequestStatusService NewRequestStatusService(FMSDBContext data)
        {
            return new Implementations.Request.RequestStatusService(data);
        }
    }
}
