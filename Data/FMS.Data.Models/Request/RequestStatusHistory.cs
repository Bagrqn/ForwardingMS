using System;

namespace FMS.Data.Models.Request
{
    public class RequestStatusHistory
    {
        public int ID { get; set; }

        public int RequestID { get; set; }

        public Request Request { get; set; }

        public int OldStatusID { get; set; }

        public RequestStatus OldRequestStatus { get; set; }

        public int NewStatusID { get; set; }

        public RequestStatus NewRequestStatus { get; set; }

        public DateTime DateChange { get; set; }
        
    }
}
