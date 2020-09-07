namespace FMS.Services
{
    public interface ICompanyService
    {
        void Create(string tame, string tulstat, string taxnumber, int countryID, int cityID, int companyTypeID, string address);

    }
}
