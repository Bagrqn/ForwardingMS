namespace FMS.Data.Models.Request
{
    public class RequestToCompany
    {
        public int RequestID { get; set; }
        
        public Request Request { get; set; }
        
        public int CompanyID { get; set; }

        public Company.Company Company { get; set; }

        public int RequestToCompanyRelationTypeID { get; set; }

        public RequestToCompanyRelationType RequestToCompanyRelationType { get; set; }
    }
}
