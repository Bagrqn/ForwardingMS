using System;

namespace FMS.Services.Models.Request
{
    public class RequestListingServiceModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public DateTime DateCreate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
