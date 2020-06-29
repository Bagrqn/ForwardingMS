namespace FMS.Data.Models.Request
{
    public class RequestToEmployee
    {
        public int RequestID { get; set; }

        public Request Request { get; set; }

        public int EmployeeID { get; set; }

        public Employee.Employee Employee { get; set; }

        public int RequestToEmployeeRelationTypeID { get; set; }

        public RequestToEmployeeRelationType RequestToEmployeeRelationType { get; set; }
    }
}
