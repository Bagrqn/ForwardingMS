using FMS.Data.Models.Document;

namespace FMS.Services.Contracts
{
    interface IDocumentTypeService
    {
        void CreateNewType(string name);

        DocumentType GetDocumentType(string name);
    }
}
