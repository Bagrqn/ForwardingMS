using System;

namespace FMS.ConsoleClient
{
    using Data.Models;
    using FMS.Data;
    using FMS.Services.Implementations;

    class Program
    {
        static void Main(string[] args)
        {
            using var data = new FMSDBContext();

            var countryService = new CountryService(data);

            var a = countryService.SearchByName("bul");


            ;
        }
    }
}
