using FMS.Data.Models.Company;

namespace FMS.Services.Factory
{
    public static class BooleanPropFactory
    {
        public static CompanyBoolProp CreateCompanyBooleanPropetry(string name, bool value, int companyID)
        {
            return new CompanyBoolProp
            {
                Name = name,
                Value = value,
                CompanyID = companyID
            };

        }
    }
}
