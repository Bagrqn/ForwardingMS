using System;

namespace FMS.ConsoleClient
{
    using Data.Models;
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var db = new FMS.Data.FMSDBContext();

            var City = new City()
            {
                Name = "Sofia"
            };

            db.SaveChanges();
        }
    }
}
