using FMS.Services.Models.Document;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface IReportService
    {
        List<InvoiceDocumentServiceModel> NotPayedInvoices();

        List<InvoiceDocumentServiceModel> PayedInvoices();

        //List<Model> RequestStatusDuration();
    }
}
