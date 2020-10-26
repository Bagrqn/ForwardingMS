using FMS.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FMS.WebClient.Controllers
{
    public class ReportController : Controller
    {
        private IReportService reportService;
        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }


        public IActionResult Reports()
        {
            return View();
        }

        public IActionResult NotPayedInvoices()
        {
            var a = reportService.Invoices(false);
            return View(a);
        }

        public IActionResult PayedInvoices()
        {
            var a = reportService.Invoices(true);
            return View(a);
        }

        public IActionResult RequestStatusDuration()
        {
            var a = reportService.RequestStatusDuration();
            return View(a);
        }
    }
}