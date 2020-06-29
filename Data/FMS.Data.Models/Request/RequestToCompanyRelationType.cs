using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Data.Models.Request
{
    public class RequestToCompanyRelationType
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(DataValidation.RequestToCompanyRelationTypeNameMaxLenght)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<RequestToCompany> RequestToCompanies { get; set; } = new HashSet<RequestToCompany>();
    }
}