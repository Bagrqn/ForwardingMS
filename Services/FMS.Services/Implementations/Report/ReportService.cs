using FMS.Data;
using FMS.Services.Contracts;
using FMS.Services.Models.Report;
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

        public List<InvoiceReportServiceModel> Invoices(bool isPayed)
        {
            var documentService = Factory.ServiceFactory.NewDocumentService(data);
            var documentTypeService = Factory.ServiceFactory.NewDocumentTypeService(data);
            var companyService = Factory.ServiceFactory.NewCompanyService(data);
            var requestService = Factory.ServiceFactory.NewRequestService(data);

            var typeInvoice = documentTypeService.GetDocumentType("Invoice");
            var list = data.Documents // list of InvoiceDocumentServiceModel
                .Where(d => d.DocumentTypeID == typeInvoice.ID)
                .Where(d => d.DocumentBoolProps.Any(p => p.Name == "IsPayed" && p.Value == isPayed))
                .Select(i => new InvoiceReportServiceModel()
                {
                    InvoiceNumber = i.DocumentNumber,
                    DateTime = documentService.GetDocumentDate(i.ID),
                    Rows = documentService.GetDocumentRows(i.ID),
                    RecieverCompany = companyService.GetCompany((int)documentService.GetDocumentNumericProp(i.ID, CommonValues.InvoiceProp_Document_PayerIDPropName)),
                    SupplierCompany = companyService.GetCompany((int)documentService.GetDocumentNumericProp(i.ID, CommonValues.InvoiceProp_Document_SupplierIDPropName)),
                    Request = requestService.GetRequest(i.RequestID)
                }).ToList();

            return list;
        }

        public List<RequestStatudDurationServiceModel> RequestStatusDuration()
        {
            var requestsIDList = data.Requests.Select(r => r.ID).ToList();
            var requestService = Factory.ServiceFactory.NewRequestService(data);
            var requestStatusService = Factory.ServiceFactory.NewRequestStatusService(data);

            var result = new List<RequestStatudDurationServiceModel>();
            foreach (var rID in requestsIDList)
            {
                var requestStatusHistory = data.RequestStatusHistories.Where(h => h.RequestID == rID).ToList();

                foreach (var log in requestStatusHistory)
                {
                    if (log.OldStatusID == requestStatusService.GetStatus(CommonValues.RequestDefaultStatusCode).ID)
                    {
                        result.Add(new RequestStatudDurationServiceModel()
                        {
                            Request = requestService.GetRequest(rID),
                            Status = requestStatusService.GetStatus(log.OldStatusID),
                            Duration = log.DateChange - DateTime.Parse(requestService.GetRequest(rID).DateCreate)
                        });
                    }
                    result.Add(new RequestStatudDurationServiceModel()
                    {
                        Request = requestService.GetRequest(rID),
                        Status = requestStatusService.GetStatus(log.NewStatusID),
                        Duration = (requestStatusHistory.FirstOrDefault(l => l.OldStatusID == log.NewStatusID) == null) ?
                                                                                                                DateTime.UtcNow - log.DateChange // if next status does not exist
                                                                                                                : requestStatusHistory.FirstOrDefault(l => l.OldStatusID == log.NewStatusID).DateChange - log.DateChange
                    });
                }
            }

            return result;
        }
    }
}
