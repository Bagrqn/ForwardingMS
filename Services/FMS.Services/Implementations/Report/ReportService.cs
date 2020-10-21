using FMS.Data;
using FMS.Services.Contracts;
using FMS.Services.Models.Document;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMS.Services.Implementations.Report
{
    public class ReportService : IReportService
    {
        private FMSDBContext data;
        public ReportService(FMSDBContext data)
            => this.data = data;

        public List<InvoiceDocumentServiceModel> NotPayedInvoices()
        {
            var documentService = Factory.ServiceFactory.NewDocumentService(data);
            var documentTypeService = Factory.ServiceFactory.NewDocumentTypeService(data);
            var companyService = Factory.ServiceFactory.NewCompanyService(data);
            var typeInvoice = documentTypeService.GetDocumentType("Invoice");
            var list = data.Documents
                .Where(d => d.DocumentTypeID == typeInvoice.ID)
                .Where(d => d.DocumentBoolProps.Any(p => p.Name == "IsPayed" && p.Value == false))
                .Select(i => new InvoiceDocumentServiceModel() 
                {
                    InvoiceNumber = i.DocumentNumber,
                    DateTime = documentService.GetDocumentDate(i.ID),
                    Rows = documentService.GetDocumentRows(i.ID),
                    RecieverCompany = companyService.GetCompany((int)documentService.GetDocumentNumericProp(i.ID, "Payer-ID"))
                }).ToList();
            ;
            return list;
        }

        public List<InvoiceDocumentServiceModel> PayedInvoices()
        {
            throw new NotImplementedException();
        }
    }
}
