namespace FMS.ConsoleClient
{
    using FMS.Data;

    class Program
    {
        static void Main(string[] args)
        {
            using var data = new FMSDBContext();
            new DataSeeder(data).Seed();



            //var a = service.SearchByName("X9R");
            /*
            service.Create(1, Data.Models.Request.LoadingUnloadingPointTypeEnum.Loading, 1, 1, 1, "Bukva ЩТРЪ", 1);
            service.Create(1, Data.Models.Request.LoadingUnloadingPointTypeEnum.Unloading, 2, 2, 2, "Друга буква ЩТРЪ", 1);
            
            var companyService = new CompanyService(data);
                        
            var cityService = new CityService(data);

            companyService.Delete(1);

            //var a = cityService.SearchByName("sof").ToList();
            */


        }
    }
}
