using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using FMS.Services.Factory;
using FMS.Services.Implementations;
using FMS.Services.Implementations.Request;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace FMS.ConsoleClient
{
    public class DataSeeder
    {
        private FMSDBContext data;
        private static Random random = new Random();

        public DataSeeder(FMSDBContext data)
        => this.data = data;

        public void Seed()
        {
            Console.WriteLine("DocumentTypesSeed...");
            DocumentTypesSeed();
            Console.WriteLine("Done! ");

            Console.WriteLine("SettingsSeeder...");
            SettingsSeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("RequestTypeSeeder...");
            RequestTypeSeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("RequestStatusesSeeder...");
            RequestStatusesSeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("CountrySeeder...");
            CountrySeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("CitySeeder...");
            CitySeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("PostCodeSeeder...");
            PostCodeSeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("CompanyTypeSeeder...");
            CompanyTypeSeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("CompanySeeder...");
            CompanySeeder();
            Console.WriteLine("Done! ");

            Console.WriteLine("GenderSeed...");
            GenderSeed();
            Console.WriteLine("Done! ");

            Console.WriteLine("EmployeeSeed...");
            EmployeeSeed();
            Console.WriteLine("Done! ");

            Console.WriteLine("LoadPackageTypeSeeder...");
            LoadPackageTypeSeeder();
            Console.WriteLine("Done! ");


        }

        private void DocumentTypesSeed()
        {
            var s = ServiceFactory.NewDocumentTypeService(data);
            s.CreateNewType("Invoice");
            s.CreateNewType("CMR");
        }

        private void SettingsSeeder()
        {

            var s = new SettingService(data);
            s.CreateSetting("RequestStatusSettingFilePath", @"C:\Users\Bagrqn\source\repos\FMS-Repo\Services\FMS.Services\Settings\RequestStatus\RequestStatusSetting.json", "");
        }

        private void RequestStatusesSeeder()
        {
            var service = new RequestStatusService(data);

            service.Create(0d, "Default", "First status for every request.");
            service.Create(1, "Acepted", "Клиента е потвърдил поръчката");
            service.Create(2, "Invoiced", "Фактурирано, но не платено.");
            service.Create(3, "Payed", "Платено");
            service.Create(9, "Deleted", "Отхвърлена заявка");

        }

        private void LoadPackageTypeSeeder()
        {
            var service = new Services.Implementations.Load.LoadService(data);
            service.CreatePackageType("Pallet");
            service.CreatePackageType("Box");
            service.CreatePackageType("Carton");
        }

        private void RequestTypeSeeder()
        {
            var a = new Services.Implementations.Request.RequestTypeService(data);
            a.Create("Transport", "");
        }

        /// <summary>
        /// Before this must have: Countries, Cities, CompanyTypes
        /// </summary>
        private void CompanySeeder()
        {
            string filePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\Companies.json";
            string fileText = File.ReadAllText(filePath);

            var json = JObject.Parse(fileText);
            var companiesList = json.GetValue("companies").ToList().Select(c => c.ToString()).ToList();


            var companyService = new CompanyService(data);
            var countriesIDList = new CountryService(data).GetAllIDs().ToList();

            var typeIDsList = new CompanyTypeService(data).GetIDs().ToList();

            foreach (var companyName in companiesList)
            {
                string bulstat = "BG0" + RandomString(12);
                int countryIDPositionInList = random.Next(0, countriesIDList.Count - 1);
                int countryID = countriesIDList[countryIDPositionInList];

                var citiesIDListByCountry = new CityService(data).GetAllIDsByCountry(countryID).ToList();
                if (citiesIDListByCountry.Count == 0)
                {
                    continue;
                }
                int cityIDPositionInList = random.Next(0, citiesIDListByCountry.Count - 1);
                int cityID = citiesIDListByCountry[cityIDPositionInList];

                int typePosition = random.Next(0, typeIDsList.Count);
                int typeID = typeIDsList[typePosition];
                try
                {
                    companyService.Create(
                        companyName
                        , bulstat
                        , ""
                        , countryID
                        , cityID
                        , typeID
                        , RandomString(random.Next(15, 50))
                        );
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void CompanyTypeSeeder()
        {
            var service = new CompanyTypeService(data);
            try
            {
                service.Create("Counterparty", "Контрагент: лице или дружество, което поема определени задължения по договор.");
                service.Create("Client", "Клиент");
                service.Create("Supplyer", "Доставчик");
                service.Create("Distributor", "Дистрибутор");
                service.Create("Carrier", "Превозвач");
            }
            catch (Exception)
            {

            }
        }

        private void CountrySeeder()
        {
            string filePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\CountriesAndCities.json";
            string fileText = File.ReadAllText(filePath);

            var json = JObject.Parse(fileText);

            var countriesList = json.Children().Select(i => i.Path).ToList()
                .Select(cn => cn.Replace("'", "").Replace("[", "").Replace("]", "")).ToList();

            var service = new CountryService(data);
            //Default
            service.Create("Undefined");

            foreach (var country in countriesList)
            {
                try
                {
                    service.Create(country);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Before this must have: Countries
        /// </summary>
        private void CitySeeder()
        {
            string filePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\CountriesAndCities.json";
            string fileText = File.ReadAllText(filePath);

            var json = JObject.Parse(fileText);

            var countriesList = json.Children().Select(i => i.Path).ToList()
               .Select(cn => cn.Replace("'", "").Replace("[", "").Replace("]", "")).ToList();

            var service = new CityService(data);
            var countryService = new CountryService(data);
            foreach (var country in countriesList)
            {
                //If country is not one of this, seeder will not inset city for it.
                if (!(country == "Bulgaria" || country == "Italy" || country == "Greece" || country == "Spain"))
                {
                    continue;
                }
                int countryID = countryService.SearchByName(country).FirstOrDefault().ID;
                int citiesCount = 0;
                try
                {
                    var cities = json.GetValue(country).ToList();
                    citiesCount = cities.Count;
                    foreach (string city in cities)
                    {
                        try
                        {
                            service.Create(city, countryID);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
                Console.WriteLine($"CountryID: {countryID} -> Cities count {citiesCount}");
            }
        }

        /// <summary>
        /// Before this must have: Cities
        /// </summary>
        private void PostCodeSeeder()
        {
            var service = new PostcodeService(data);
            var cityIDList = new CityService(data).GetAllIDs();


            foreach (var cityID in cityIDList)
            {
                if (service.AnyCodeForCity(cityID))
                {
                    Console.WriteLine(cityID);
                    continue;
                }
                int a = random.Next(5, 10);
                for (int i = 0; i < a; i++)
                {
                    string code = RandomString(4);
                    service.Create(code, cityID);
                }
                Console.WriteLine(cityID);
            }
        }

        private void GenderSeed()
        {
            var service = new GenderService(data);
            try
            {
                service.Create("Undefined");
                service.Create("Male");
                service.Create("Female");
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        ///  Before this must have: Gender
        /// </summary>
        private void EmployeeSeed()
        {
            string FirstNamesFilePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\FirstNames.json";
            string FirstNamesFileText = File.ReadAllText(FirstNamesFilePath);
            var FirstNamesList = JObject.Parse(FirstNamesFileText).GetValue("firstNames").ToList().Select(fn => fn.ToString()).ToList();

            string MiddleNamesFilePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\MiddleNames.json";
            string MiddleNamesFileText = File.ReadAllText(MiddleNamesFilePath);
            var MiddleNamesList = JObject.Parse(MiddleNamesFileText).GetValue("middleNames").ToList().Select(fn => fn.ToString()).ToList();

            string LastNamesFilePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\LastNames.json";
            string LatsNamesFileText = File.ReadAllText(LastNamesFilePath);
            var LastNamesList = JObject.Parse(LatsNamesFileText).GetValue("lastNames").ToList().Select(fn => fn.ToString()).ToList();

            var service = new EmployeeService(data);


            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    service.Create(
                    FirstNamesList[random.Next(0, FirstNamesList.Count - 1)],
                    MiddleNamesList[random.Next(0, MiddleNamesList.Count - 1)],
                    LastNamesList[random.Next(0, LastNamesList.Count - 1)],
                    RandomNumericString(10),
                    RandomDay(),
                    random.Next(2, 4)// this returns 2 or 3
                    );
                }
                catch (Exception)
                {
                    continue;
                }

            }

        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string RandomNumericString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private DateTime RandomDay()
        {
            DateTime start = new DateTime(1955, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }
    }
}
