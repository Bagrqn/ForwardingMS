using FMS.Services.Models.Company;
using System.Collections.Generic;

namespace FMS.Services
{
    interface ICompanyType
    {
        void Create(string name, string description);

        IEnumerable<CompanyTypeListingModel> ListAll();

        IEnumerable<CompanyTypeListingModel> FindByName(string name);
    }
}
