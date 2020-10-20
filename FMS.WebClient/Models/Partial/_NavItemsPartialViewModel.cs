using FMS.Data;
using FMS.Services.Implementations;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FMS.WebClient.Models.Partial
{
    public class _NavItemsPartialViewModel
    {
        public List<_NavItemPartialViewModel> loggedUserNavItems
        {
            get
            {
                //read json 
                //Get info from Json settings 
                var db = new FMSDBContext();
                string filePath = new SettingService(db).GetSetting("_navItemsPartialSettingsFilePath");
                string fileText = File.ReadAllText(filePath);
                var json = JObject.Parse(fileText);
                var listItems = json["NavItemsLoggedUser"].Children().Select(li => new _NavItemPartialViewModel()
                {
                    Text = li["text"].ToString(),
                    View = li["view"].ToString(),
                    StatusCode = li["requestStatusFilter"].ToString()
                }).ToList();


                return listItems;
            }
        }

        public List<_NavItemPartialViewModel> notLoggedUserNavItems
        {
            get
            {
                //read json 
                //Get info from Json settings 
                var db = new FMSDBContext();
                string filePath = new SettingService(db).GetSetting("_navItemsPartialSettingsFilePath");
                string fileText = File.ReadAllText(filePath);
                var json = JObject.Parse(fileText);
                var listItems = json["NavItemsNotLogged"].Children().Select(li => new _NavItemPartialViewModel()
                {
                    Text = li["text"].ToString(),
                    View = li["view"].ToString()
                }).ToList();


                return listItems;
            }
        }
    }
}
