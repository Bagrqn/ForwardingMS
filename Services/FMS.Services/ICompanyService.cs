using FMS.Services.Models.Company;
using System.Collections.Generic;

namespace FMS.Services
{
    public interface ICompanyService
    {
        void Create(string name, string bulstat, string taxnumber, int countryID, int cityID, int companyTypeID, string address);

        void Delete(int id);

        IEnumerable<CompanyListingServiceModel> GetCarrierCompanies();
        IEnumerable<CompanyListingServiceModel> GetPayerCompanies();
    }
}
