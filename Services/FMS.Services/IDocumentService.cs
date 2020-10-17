using FMS.Services.Models.Document;
using System.Collections.Generic;

namespace FMS.Services
{
    public interface IDocumentService
    {
        void CreateInvoice(int requestID);

        InvoiceDocumentServiceModel GetInvoice(int requetsID);

        List<DocumentRowListingServiceModel> GetDocumentRows(int DocumentID);
        void ConfirmInvoice(int requestID);
    }
}
