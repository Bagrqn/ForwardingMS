namespace FMS.Data.Models
{
    public static class DataValidation
    {
        /// <summary>
        /// Validation for every propery table. 
        /// </summary>
        public const int PropetryNameMaxLenght = 30;
        public const int NameMaxLenght = 30;

        public const int CountryNameMaxLenght = 50;
        public const int CityNameMaxLenght = 50;
        public const int AddressMaxLenght = 100;
        public const int PostcodeMaxLenght = 16;
        //Request
        public const int RequestTypeNameMaxLenght = 30;

        public const int RequestToCompanyRelationTypeNameMaxLenght = 30;
        public const int RequestToEmployeeRelationTypeNameMaxLenght = 30;

        public const int RequestStatusNameMaxLenght = 30;


        //Company
        public const int CompanyNameMaxLenght = 80;
        public const int CompanyBulstatMaxLenght = 15;
        public const int CompanyTaxNumberMaxLenght = 20;
        public const int CompanyAddresMaxLenght = 120;
        public const int CompanyTypeNameMaxLenght = 30;

        //Employee
        public const int EmployeeNameMaxLenght = 50;
        public const int EmployeeEGNMaxLenght = 10;

        //Document
        public const int DocumentNumberMaxLenght = 50;
        public const int DocumentTypeNameMaxLenght = 30;
    }
}
