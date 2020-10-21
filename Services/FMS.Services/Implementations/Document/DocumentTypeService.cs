using FMS.Data;
using FMS.Data.Models.Document;
using FMS.Services.Contracts;
using System;
using System.Linq;

namespace FMS.Services.Implementations.Document
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private FMSDBContext data;

        public DocumentTypeService(FMSDBContext data)
            => this.data = data;

        public void CreateNewType(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Name of document type can not be null or empty. ");
            }
            data.DocumentTypes.Add(new DocumentType() { Name = name });
            data.SaveChanges();
        }

        public DocumentType GetDocumentType(string name)
        {
            return data.DocumentTypes.FirstOrDefault(t => t.Name == name);
        }
    }
}
