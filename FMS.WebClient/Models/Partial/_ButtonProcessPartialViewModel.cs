using FMS.Data;
using FMS.Services.Factory;
using FMS.Services.Implementations;
using FMS.Services.Models.Request;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace FMS.WebClient.Models.Partial
{
    public class _ButtonProcessPartialViewModel
    {
        public int RequestID { get; set; }

        public int RequestNextStatusID
        {
            get
            {
                var db = new FMSDBContext();
                return ServiceFactory.NewRequestService(db).GetNextStatus(RequestID);
            }
        }
        public RequestStatusServiceModel RequestNextStatus
        {
            get
            {
                var db = new FMSDBContext();
                return ServiceFactory.NewRequestStatusService(db).GetStatus(RequestNextStatusID);
            }
        }
        public string NextView
        {
            get
            {
                //Get info from Json settings 
                var db = new FMSDBContext();
                string filePath = new SettingService(db).GetSetting("_buttonProcessPartialSettingsFilePath");
                string fileText = File.ReadAllText(filePath);
                var json = JObject.Parse(fileText);
                var settings = json["settings"].Children().FirstOrDefault(x => (double)x["nextStatusCode"] == RequestNextStatus.Code);
                string view = settings["view"].ToString();

                return view;
            }
        }

        public string btnText
        {
            get
            {
                //Get info from Json settings 
                var db = new FMSDBContext();
                string filePath = new SettingService(db).GetSetting("_buttonProcessPartialSettingsFilePath");
                string fileText = File.ReadAllText(filePath);
                var json = JObject.Parse(fileText);
                var settings = json["settings"].Children().FirstOrDefault(x => (double)x["nextStatusCode"] == RequestNextStatus.Code);
                string btnText = settings["btnText"].ToString();

                return btnText;
            }
        }
    }
}
