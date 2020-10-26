using FMS.Services.Models.Request;
using System;

namespace FMS.Services.Models.Report
{
    public class RequestStatudDurationServiceModel
    {
        public FullInfoRequestServiceModel Request { get; set; }

        public RequestStatusServiceModel Status { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
