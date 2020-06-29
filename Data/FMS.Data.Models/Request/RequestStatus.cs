using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class RequestStatus
    {
        public int ID { get; set; }

        public int Code { get; set; }

        [Required]
        [MaxLength(DataValidation.RequestStatusNameMaxLenght)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Request> Requests { get; set; } = new HashSet<Request>();

        public ICollection<RequestStatusHistory> OldRequestStatusHistories { get; set; } = new HashSet<RequestStatusHistory>();
        
        public ICollection<RequestStatusHistory> NewRequestStatusHistories { get; set; } = new HashSet<RequestStatusHistory>();
    }
}
