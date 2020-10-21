using FMS.Services.Models.Company;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    interface ICompanyTypeService
    {
        void Create(string name, string description);

        IEnumerable<CompanyTypeListingModel> ListAll();

        IEnumerable<CompanyTypeListingModel> FindByName(string name);
    }
}
