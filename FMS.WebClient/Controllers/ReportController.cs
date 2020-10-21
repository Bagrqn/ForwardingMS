using FMS.Services;
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
            var a = reportService.NotPayedInvoices();
            return View(a);
        }

        public IActionResult PayedInvoices()
        {
            return View();
        }

        public IActionResult RequestStatusDuration()
        {
            return View();
        }
    }
}