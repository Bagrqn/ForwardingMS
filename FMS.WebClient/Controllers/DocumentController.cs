using FMS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FMS.WebClient.Controllers
{
    public class DocumentController : Controller
    {
        private IDocumentService documentService;
        private IRequestService requestService;
        public DocumentController(IDocumentService documentService, IRequestService requestService)
        {
            this.documentService = documentService;
            this.requestService = requestService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateInvoice(int requestID)
        {
            documentService.CreateInvoice(requestID);
            var invoice = documentService.GetInvoice(requestID);
            ViewData.Model = invoice;
            ViewData.Add("requestID", requestID);
            return View();
        }

        public IActionResult ConfirmInvoice(int requestID)
        {
            documentService.ConfirmInvoice(requestID);
            requestService.ProcessToNextStatus(requestID);
            return Redirect("/");
        }

        public IActionResult ProcessInvoiceToPayed(int requestID)
        {
            requestService.ProcessToPayed(requestID);
            requestService.ProcessToNextStatus(requestID);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}