using FMS.Data.Models.Document;

namespace FMS.Services
{
    interface IDocumentTypeService
    {
        void CreateNewType(string name);

        DocumentType GetDocumentType(string name);
    }
}
