using FMS.Data;
using FMS.Data.Models.Document;
using FMS.Services.Models.Document;
using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace FMS.Services.Implementations.Document
{
    public class DocumentService : IDocumentService
    {
        private FMSDBContext data;
        public DocumentService(FMSDBContext data)
            => this.data = data;
        
        
        public void CreateInvoice(int requestID)
        {
            var invoiceType = Factory.ServiceFactory.NewDocumentTypeService(data).GetDocumentType("Invoice");
            if (invoiceType == null)
            {
                Factory.ServiceFactory.NewDocumentTypeService(data).CreateNewType("Invoice");
                invoiceType = Factory.ServiceFactory.NewDocumentTypeService(data).GetDocumentType("Invoice");
            }
            //If invoice already exist.
            var existingInvoice = data.Documents.FirstOrDefault(d => d.RequestID == requestID && d.DocumentTypeID == invoiceType.ID);
            if (existingInvoice != null)
            {
                return;
            }
            //document header
            var invoice = new Data.Models.Document.Document()
            {
                RequestID = requestID,
                DocumentNumber = GetDocumentNumber(),
                DocumentTypeID = invoiceType.ID,
                DocumentNumericProps = new List<DocumentNumericProp>(),
                DocumentStringProps = new List<DocumentStringProp>(),
                DocumentBoolProps = new List<DocumentBoolProp>(),
                DocumentRows = new List<DocumentRow>()
            };
            invoice.DocumentStringProps.Add(NewDocumentProp("DateCreate", DateTime.UtcNow.ToString()));
            //invoice props
            //payerProsp
            var relationType = Factory.ServiceFactory.NewCompanyService(data).GetRequestToCompanyRelationType("Payer");
            var relation = data.RequestToCompanies.FirstOrDefault(x => x.RequestID == requestID && x.RequestToCompanyRelationTypeID == relationType.ID);
            if (relation == null)
            {
                throw new InvalidOperationException("There is no payer for this request.");
            }
            var client = Factory.ServiceFactory.NewCompanyService(data).GetCompany(relation.CompanyID);
            var mol = client.StringProps.FirstOrDefault(p => p.Name == "MOL");

            invoice.DocumentNumericProps.Add(NewDocumentProp("Payer-ID", relation.CompanyID));
            invoice.DocumentStringProps.Add(NewDocumentProp("Payer-Name", client.Name));
            invoice.DocumentStringProps.Add(NewDocumentProp("Payer-Address", client.Address));
            invoice.DocumentStringProps.Add(NewDocumentProp("Payer-Bulstat", client.Bulstat));
            invoice.DocumentStringProps.Add(NewDocumentProp("Payer-TaxNumber", client.TaxNumber));
            invoice.DocumentStringProps.Add(NewDocumentProp("Payer-MOL", mol == null ? "-" : mol.Value));

            //supplierProps
            relationType = Factory.ServiceFactory.NewCompanyService(data).GetRequestToCompanyRelationType("Main");
            relation = data.RequestToCompanies.FirstOrDefault(x => x.RequestID == requestID && x.RequestToCompanyRelationTypeID == relationType.ID);
            if (relation == null)
            {
                throw new InvalidOperationException("There is no supplier for this request.");
            }
            var supplier = Factory.ServiceFactory.NewCompanyService(data).GetCompany(relation.CompanyID);
            mol = supplier.StringProps.FirstOrDefault(p => p.Name == "MOL");

            invoice.DocumentNumericProps.Add(NewDocumentProp("Supplier-ID", relation.CompanyID));// Main company
            invoice.DocumentStringProps.Add(NewDocumentProp("Supplier-Name", supplier.Name));
            invoice.DocumentStringProps.Add(NewDocumentProp("Supplier-Address", supplier.Address));
            invoice.DocumentStringProps.Add(NewDocumentProp("Supplier-Bulstat", supplier.Bulstat == null ? "-" : supplier.Bulstat));
            invoice.DocumentStringProps.Add(NewDocumentProp("Supplier-TaxNumber", supplier.TaxNumber == null ? "-" : supplier.TaxNumber));
            invoice.DocumentStringProps.Add(NewDocumentProp("Supplier-MOL", mol == null ? "-" : mol.Value));

            //rows
            var clientPrice = data.RequestNumericProps.FirstOrDefault(p => p.RequestID == requestID && p.Name == "ClientPrice");

            invoice.DocumentRows.Add(AddDocumentRow(invoice, "Transport", 1, clientPrice.Value));


            data.Documents.Add(invoice);
            data.SaveChanges();


        }
        public InvoiceDocumentServiceModel GetInvoice(int requetsID)
        {
            var typeService = Factory.ServiceFactory.NewDocumentTypeService(data);
            var invoiceHeader = data.Documents.FirstOrDefault(d => d.RequestID == requetsID && d.DocumentTypeID == typeService.GetDocumentType("Invoice").ID);

            var result = new InvoiceDocumentServiceModel()
            {
                InvoiceNumber = invoiceHeader.DocumentNumber,
                DateTime = GetDocumentDate(invoiceHeader.ID),
                RecieverCompany = Factory.ServiceFactory.NewCompanyService(data).GetCompany((int)GetDocumentNumericProp(invoiceHeader.ID, "Payer-ID")),
                SupplierCompany = Factory.ServiceFactory.NewCompanyService(data).GetCompany((int)GetDocumentNumericProp(invoiceHeader.ID, "Supplier-ID")),
                Rows = GetDocumentRows(invoiceHeader.ID)
            };

            return result;
        }
        public List<DocumentRowListingServiceModel> GetDocumentRows(int DocumentID)
        {
            var rows = data.DocumentRows
                .Where(r => r.DocumentID == DocumentID)
                .Select(r => new DocumentRowListingServiceModel()
                {
                    RowID = r.ID,
                    RowNumber = r.RowNumber,
                    DocumentRowNumericProps = data.DocumentRowNumericProps
                                                    .Where(p => p.DocumentRowID == r.ID).ToList(),
                    DocumentRowBooleanProps = data.DocumentRowBooleanProps
                                                    .Where(p => p.DocumentRowID == r.ID).ToList(),
                    DocumentRowStringProps = data.DocumentRowStringProps
                                                    .Where(p => p.DocumentRowID == r.ID).ToList()
                }).ToList();
            ;
            return rows;
        }
        public void ConfirmInvoice(int requestID)
        {
            var typeService = Factory.ServiceFactory.NewDocumentTypeService(data);
            var invoice = data.Documents.FirstOrDefault(d => d.RequestID == requestID && d.DocumentTypeID == typeService.GetDocumentType("Invoice").ID);
            var confirmedProp = NewDocumentProp("IsConfirmed", true);
            //проверка дали няма такова проп, ако има само да го update;
            invoice.DocumentBoolProps.Add(confirmedProp);
            invoice.DocumentStringProps.Add(NewDocumentProp("DateConfirm", DateTime.UtcNow.ToString()));
            data.SaveChanges();
        }

        private DocumentStringProp NewDocumentProp(string name, string value)
        {
            return new DocumentStringProp()
            {
                Name = name,
                Value = value
            };
        }
        private DocumentNumericProp NewDocumentProp(string name, double value)
        {
            return new DocumentNumericProp()
            {
                Name = name,
                Value = value
            };
        }
        private DocumentBoolProp NewDocumentProp(string name, bool value)
        {
            return new DocumentBoolProp()
            {
                Name = name,
                Value = value
            };
        }
        private DocumentRow AddDocumentRow(FMS.Data.Models.Document.Document document, string rowDescription, int qty, double unitPrice)
        {
            var row = new DocumentRow()
            {
                RowNumber = document.DocumentRows.Count + 1,//get next number
                DocumentRowNumericProps = new List<DocumentRowNumericProp>(),
                DocumentRowStringProps = new List<DocumentRowStringProp>(),
                DocumentRowBooleanProps = new List<DocumentRowBooleanProp>()
            };

            //Row props

            row.DocumentRowStringProps.Add(AddRowProp("RowDescription", rowDescription));
            row.DocumentRowNumericProps.Add(AddRowProp("Qty", Convert.ToDouble(qty)));
            row.DocumentRowNumericProps.Add(AddRowProp("UnitPrice", unitPrice));
            row.DocumentRowNumericProps.Add(AddRowProp("TotalPrice", qty * unitPrice));

            return row;
        }
        private DocumentRowStringProp AddRowProp(string propName, string propValue)
        {
            return new DocumentRowStringProp() { Name = propName, Value = propValue };
        }
        private DocumentRowNumericProp AddRowProp(string propName, double propValue)
        {
            return new DocumentRowNumericProp() { Name = propName, Value = propValue };
        }
        private double GetDocumentNumericProp(int documentID, string propName)
        {
            var prop = data.DocumentNumericProps.FirstOrDefault(p => p.DocumentID == documentID && p.Name == propName);
            if (prop != null)
            {
                return prop.Value;
            }
            throw new InvalidOperationException("Property das not exist");
        }
        private string GetDocumentDate(int documentID)
        {
            return data.DocumentStringProps.FirstOrDefault(d => d.DocumentID == documentID && d.Name == "DateCreate").Value;
        }
        private string GetDocumentNumber()
        {
            string num = DateTime.UtcNow.Year.ToString()
                + DateTime.UtcNow.Month.ToString()
                + DateTime.UtcNow.Day.ToString()
                + DateTime.UtcNow.Hour.ToString()
                + DateTime.UtcNow.Minute.ToString()
                + DateTime.UtcNow.Second.ToString();
            return num;
        }

       
    }

}

