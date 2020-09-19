using FMS.Data;
using FMS.Services.Implementations;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

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
            //CountrySeeder(); //Done
            //CitySeeder(); //Take too much time - 84k rows // Done
            //PostCodeSeeder(); // From 5 to 10 rows foreach city 

            //CompanyTypeSeeder(); //Done
            //CompanySeeder(); //Done

            //GenderSeed(); //Done

            EmployeeSeed();


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
                string bulstat = "BG0" + randomString(12);
                int countryIDPositionInList = random.Next(0, countriesIDList.Count - 1);
                int countryID = countriesIDList[countryIDPositionInList];

                var citiesIDListByCountry = new CityService(data).GetAllIDsByCountry(countryID).ToList();
                if (citiesIDListByCountry.Count == 0)
                {
                    continue;
                }
                int cityIDPositionInList = random.Next(0, citiesIDListByCountry.Count - 1);
                int cityID = citiesIDListByCountry[cityIDPositionInList];

                int typePosition = random.Next(0, typeIDsList.Count - 1);
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
                        , randomString(random.Next(15, 50))
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

            service.Create("Counterparty", "Контрагент: лице или дружество, което поема определени задължения по договор.");
            service.Create("Client", "Клиент");
            service.Create("Supplyer", "Доставчик");
            service.Create("Distributor", "Дистрибутор");
            service.Create("Carrier", "Превозвач");
        }

        private void CountrySeeder()
        {
            string filePath = @"C:\Users\Bagrqn\source\repos\FMS-Repo\FMS.ConsoleClient\DataFiles\CountriesAndCities.json";
            string fileText = File.ReadAllText(filePath);

            var json = JObject.Parse(fileText);

            var countriesList = json.Children().Select(i => i.Path).ToList()
                .Select(cn => cn.Replace("'", "").Replace("[", "").Replace("]", "")).ToList();

            var service = new CountryService(data);

            foreach (var country in countriesList)
            {
                service.Create(country);
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
                for (int i = 0; i < random.Next(5, 10); i++)
                {
                    string code = randomString(4);
                    service.Create(code, cityID);
                }
            }
        }

        private void GenderSeed()
        {
            var service = new GenderService(data);

            service.Create("Male");
            service.Create("Female");
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



        }
        private string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string randomNumericString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
