using FMS.Data.Models.Document;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class Request
    {
        public int ID { get; set; }

        [Required]
        public string Number { get; set; }

        public bool IsDeleted { get; set; }

        public int RequestTypeID { get; set; }

        public RequestType RequestType { get; set; }

        public ICollection<Document.Document> Documents { get; set; } = new HashSet<Document.Document>();

        public ICollection<RequestToCompany> RequestToCompanies { get; set; } = new HashSet<RequestToCompany>();

        public ICollection<RequestToEmployee> RequestToEmployees { get; set; } = new HashSet<RequestToEmployee>();

    }
}
