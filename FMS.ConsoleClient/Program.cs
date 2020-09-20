namespace FMS.ConsoleClient
{
    using FMS.Data;
    using FMS.Services.Implementations;
    using FMS.Services.Implementations.Request;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            
            using var data = new FMSDBContext();

            var a = new RequestStatusService(data).GetDefaultStatusID();
            new RequestStatusService(data).Create(1.2, "Status 2 ", "Test Default status create. ");
            var b = new RequestStatusService(data).GetStatusIDByCode(1.1);

            //new DataSeeder(data).Seed();
            /*
            
            var companyService = new CompanyService(data);
                        
            var cityService = new CityService(data);

            companyService.Delete(1);

            //var a = cityService.SearchByName("sof").ToList();
            */

            ;
        }
    }
}
