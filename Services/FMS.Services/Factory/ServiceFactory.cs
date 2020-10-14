using FMS.Data;
using FMS.Services.Implementations;
using FMS.Services.Implementations.Document;
using FMS.Services.Implementations.Load;
using FMS.Services.Implementations.Request;
using System;

namespace FMS.Services.Factory
{
    public class ServiceFactory
    {
        public static RequestTypeService NewRequestTypeService(FMSDBContext data)
        {
            return new RequestTypeService(data);
        }

        public static RequestStatusService NewRequestStatusService(FMSDBContext data)
        {
            return new RequestStatusService(data);
        }

        public static RequestService NewRequestService(FMSDBContext data)
        {
            return new RequestService(data);
        }

        public static LoadService NewLoadService(FMSDBContext data)
        {
            return new LoadService(data);
        }

        public static CompanyService NewCompanyService(FMSDBContext data)
        {
            return new CompanyService(data);
        }

        public static CityService NewCityService(FMSDBContext data)
        {
            return new CityService(data);
        }

        public static DocumentTypeService NewDocumentTypeService(FMSDBContext data)
        {
            return new DocumentTypeService(data);
        }

        public static DocumentService NewDocumentService(FMSDBContext data)
        {
            return new DocumentService(data);
        }
    }
}
