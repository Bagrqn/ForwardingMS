using FMS.Services.Models.Request;
using System;
using System.Collections.Generic;

namespace FMS.WebClient.Models
{
    public class RequestListingViewModel
    {
        public IEnumerable<BasicRequestsLintingServiceModel> list { get; set; }

        public double? StatusCode { get; set; }

        public RequestStatusServiceModel Status { get; set; }

        public int CountByStatus { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage - 1;

        public int NextPage => this.CurrentPage + 1;

        public bool PreviousDisabled => this.CurrentPage == 1;

        public bool NextDisabled
        {
            get
            {
                var maxPage = Math.Ceiling((double)CountByStatus / 20); //20 is page size. To do... expose to settings 
                if (maxPage == 0)
                {
                    return true;
                }
                return CurrentPage == maxPage;
            }
        }

    }
}
