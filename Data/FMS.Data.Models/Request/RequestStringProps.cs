namespace FMS.Data.Models.Request
{
    public class RequestStringProps
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public int RequestID { get; set; }

        public Request Request { get; set; }
    }
}
