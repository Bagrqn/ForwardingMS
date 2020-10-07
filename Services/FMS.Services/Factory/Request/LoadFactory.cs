using FMS.Data.Models.Request;

namespace FMS.Services.Factory.Request
{
    public class LoadFactory
    {
        public static Load Create(string name, string comment, int packageTypeID, int packageCount)
        {
            return new Data.Models.Request.Load()
            {
                Name = name,
                Comment = comment,
                PackageTypeID = packageTypeID,
                PackageCount = packageCount
            };
        }

        public static LoadStringProp NewStringProp(string name, string value)
        {
            return new Data.Models.Request.LoadStringProp()
            {
                Name = name,
                Value = value
            };
        }

        public static LoadNumericProps NewNumericProps(string name, double value)
        {
            return new LoadNumericProps()
            {
                Name = name,
                Value = value
            };
        }
    }
}
