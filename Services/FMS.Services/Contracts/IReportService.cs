using FMS.Services.Models.Report;
using System.Collections.Generic;

namespace FMS.Services.Contracts
{
    public interface IReportService
    {
        List<InvoiceReportServiceModel> Invoices(bool isPayed);

        List<RequestStatudDurationServiceModel> RequestStatusDuration();
    }
}
