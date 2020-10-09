namespace FMS.Services.Models.Request
{
    public class BasicRequestsLintingServiceModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string DateCreate { get; set; }
        public string FromCountryCity { get; set; } //{Country}+"-"+{City}
        public string ToCountryCity { get; set; } //{Country}+"-"+{City}
        public string LoadName { get; set; }
    }
}
