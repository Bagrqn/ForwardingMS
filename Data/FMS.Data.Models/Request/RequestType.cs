using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class RequestType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.RequestTypeNameMaxLenght)]
        public string Name { get; set; }

        public ICollection<Request> Requests { get; set; }
    }
}