using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class RequestToEmployeeRelationType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.RequestToEmployeeRelationTypeNameMaxLenght)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RequestToEmployee> RequestToEmployees { get; set; } = new HashSet<RequestToEmployee>();
    }
}