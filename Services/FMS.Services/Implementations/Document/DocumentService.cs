using FMS.Data;
using System.Linq;

namespace FMS.Services.Implementations.Document
{
    public class DocumentService : IDocumentService
    {
        private FMSDBContext data;
        public DocumentService(FMSDBContext data)
            => this.data = data;
        public void CreateInvoice(int requestID)
        {
            //
            var customerPriceFromRequest = data.RequestNumericProps.FirstOrDefault(np => np.Name == "ClientPrice" && np.RequestID == requestID);

        }
    }
}
