namespace FMS.Services.Contracts
{
    public interface IRequestTypeService
    {
        void Create(string name, string description);

        void Delete(int requestTypeID);

        public Models.Request.RequestTypeServiceModel FindTypeByName(string typeName);
        
        void CheckForNewTypes();
    }
}
