namespace FMS.ConsoleClient
{
    using System.Diagnostics;
    using System;
    using FMS.Data;
    using FMS.Services.Implementations;
    using FMS.Services.Implementations.Load;

    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();

            using var data = new FMSDBContext();
            var service = new CityService(data);

            stopwatch.Start();
            var a = service.GetPostcodes(553);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);

            stopwatch.Start();
            var b = service.GetPostcodes(5523);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            ;



            //var a = service.SearchByName("X9R");
            //new DataSeeder(data).Seed();
            /*
            service.Create(1, Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading, 1, 1, 1, "Bukva ЩТРЪ", 1);
            service.Create(1, Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading, 2, 2, 2, "Друга буква ЩТРЪ", 1);
            
            var companyService = new CompanyService(data);
                        
            var cityService = new CityService(data);

            companyService.Delete(1);

            //var a = cityService.SearchByName("sof").ToList();
            */

            ;
        }
    }
}
