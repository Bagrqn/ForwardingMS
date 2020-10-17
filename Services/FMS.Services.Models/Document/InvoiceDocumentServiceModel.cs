using FMS.Data.Models.Document;
using FMS.Services.Models.Company;
using System;
using System.Collections.Generic;

namespace FMS.Services.Models.Document
{
    public class InvoiceDocumentServiceModel
    {
        public CompanyListingServiceModel RecieverCompany { get; set; }

        public CompanyListingServiceModel SupplierCompany { get; set; }

        public string InvoiceNumber { get; set; }

        public string DateTime { get; set; }

        public List<DocumentRowListingServiceModel> Rows { get; set; }
    }
}
