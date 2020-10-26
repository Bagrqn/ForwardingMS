using FMS.Services.Models.Company;
using FMS.Services.Models.Document;
using FMS.Services.Models.Request;
using System.Collections.Generic;

namespace FMS.Services.Models.Report
{
    public class InvoiceReportServiceModel
    {
        public FullInfoRequestServiceModel Request { get; set; }
        
        public CompanyListingServiceModel RecieverCompany { get; set; }

        public CompanyListingServiceModel SupplierCompany { get; set; }

        public string InvoiceNumber { get; set; }

        public string DateTime { get; set; }

        public List<DocumentRowListingServiceModel> Rows { get; set; }
    }
}
