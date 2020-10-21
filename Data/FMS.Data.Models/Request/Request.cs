using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class Request
    {
        public int ID { get; set; }

        [Required]
        public string Number { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        public int RequestTypeID { get; set; }

        public RequestType RequestType { get; set; }

        public int RequestStatusID { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public ICollection<Document.Document> Documents { get; set; } = new HashSet<Document.Document>();

        public ICollection<RequestToCompany> RequestToCompanies { get; set; } = new HashSet<RequestToCompany>();

        public ICollection<RequestToEmployee> RequestToEmployees { get; set; } = new HashSet<RequestToEmployee>();

        public ICollection<RequestStatusHistory> RequestStatusHistories { get; set; } = new HashSet<RequestStatusHistory>();

        public ICollection<Load> Loads { get; set; } = new HashSet<Load>();

        public ICollection<LoadingUnloadingPoint> LoadingUnloadingPoints { get; set; } = new HashSet<LoadingUnloadingPoint>();

        public ICollection<RequestNumericProp> RequestNumericProps { get; set; } = new HashSet<RequestNumericProp>();

        public ICollection<RequestStringProps> RequestStringProps { get; set; } = new HashSet<RequestStringProps>();
    }
}
