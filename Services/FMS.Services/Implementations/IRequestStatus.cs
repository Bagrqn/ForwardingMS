namespace FMS.Services.Implementations
{
    public interface IRequestStatus
    {
        void Create(double code, string name, string description);

        /// <summary>
        /// 
        /// </summary>
        /// <returns> Status code ID </returns>
        int GetDefaultStatusID();

        int GetStatusIDByCode(double code);

    }
}
