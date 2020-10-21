using FMS.Services.Models.Document;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface IDocumentService
    {
        void CreateInvoice(int requestID);

        InvoiceDocumentServiceModel GetInvoice(int requetsID);

        List<DocumentRowListingServiceModel> GetDocumentRows(int DocumentID);

        double GetDocumentNumericProp(int documentID, string propName);

        void ConfirmInvoice(int requestID);
        
        string GetDocumentDate(int documentID);
    }
}
