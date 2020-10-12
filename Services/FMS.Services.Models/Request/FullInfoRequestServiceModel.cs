namespace FMS.Services.Models.Request
{
    public class FullInfoRequestServiceModel
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public string DateCreate { get; set; }
        public string LoadName { get; set; }

        //Country + City + Address + postcode
        public string FromInfo { get; set; }

        //Country + City + Address + postcode
        public string ToInfo { get; set; }


        //Load
        public string LoadComment { get; set; } //Comment, short description, aditional info
        public int PackageTypeID { get; set; }
        public int PackageCount { get; set; }

        //Load props
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double WeightBrut { get; set; }
        public double WeightNet { get; set; }
        public double Lademeter { get; set; }
        public string StockType { get; set; }

        //Assignor info
        public string AssignorFirstName { get; set; }
        public string AssignorLastName { get; set; }
        public string AssignorPhoneNumber { get; set; }
        public string AssignorCompanyName { get; set; }

        //Carrier company
        public int CarrierConpanyID { get; set; }

        public int PayerConpanyID { get; set; }
        
        public double CarrierPrice { get; set; }
        public double ClientPrice { get; set; }
        public double Saldo { get; set; }
    }
}
