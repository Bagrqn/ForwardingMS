namespace FMS.Services.Contracts
{
    public interface ISettingService
    {
        string GetSetting(string name);

        void CreateSetting(string name, string value, string description);

        void UpdateSetting(string name, string newValue, string newDescription);
    }
}
