using FMS.Data;
using FMS.Data.Models.Settings;
using System;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly FMSDBContext data;
        public SettingService(FMSDBContext data)
            => this.data = data;
        public void CreateSetting(string name, string value, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Setting name can not be null or empty. ");
            }
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidOperationException("Setting value can not be null or empty. ");
            }
            var setting = new Setting()
            {
                Name = name,
                Value = value,
                Description = description
            };
            data.Add(setting);
            data.SaveChanges();
        }

        public string GetSetting(string name)
        {
            return data.Settings.FirstOrDefault(s => s.Name == name).Value;
        }

        public void UpdateSetting(string name, string newValue, string newDescription)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Setting name can not be null or empty. ");
            }
            if (string.IsNullOrEmpty(newValue))
            {
                throw new InvalidOperationException("Setting value can not be null or empty. ");
            }

            var setting = data.Settings.FirstOrDefault(s => s.Name == name);
            setting.Value = newValue;
            setting.Description = newDescription;
            data.SaveChanges();
        }
    }
}
