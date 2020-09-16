namespace FMS.Services.Factory.Contracts
{
    public interface IBooleanProp
    {
        public string Name { get; set; }
        public bool Value { get; set; }
        public int RelatedObjectID { get; set; }
    }
}
