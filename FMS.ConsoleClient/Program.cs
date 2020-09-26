namespace FMS.ConsoleClient
{
    using FMS.Data;
    using FMS.Services.Implementations.Request;

    class Program
    {
        static void Main(string[] args)
        {

            using var data = new FMSDBContext();

            var service = new RequestService(data);

            
            ;
            ; 
            ;






            //var a = service.SearchByName("X9R");
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
