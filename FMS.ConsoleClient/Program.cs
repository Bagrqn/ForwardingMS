namespace FMS.ConsoleClient
{
    using FMS.Data;
    using FMS.Services.Implementations;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using var data = new FMSDBContext();

            var cityService = new CityService(data);


            var a = cityService.SearchByName("sof").ToList();


            ;
        }
    }
}
