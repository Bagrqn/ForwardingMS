using FMS.Data;
using FMS.Data.Models;
using FMS.Data.Models.Request;
using FMS.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace FMS.Services.Implementations.Request
{
    public class RequestTypeService : IRequestTypeService
    {
        private readonly FMSDBContext data;

        public RequestTypeService(FMSDBContext data)
            => this.data = data;

        public void CheckForNewTypes()
        {
            //Read setting from file for this request type.
            string filePath = new SettingService(data).GetSetting(CommonValues.Settings_RequestTypesList_SettingName);
            string fileText = File.ReadAllText(filePath);
            var json = JObject.Parse(fileText);

            var typesListFromJson = json["RequestTypes"].Children().ToList()
                .Select(s => new
                {
                    Name = s["Name"].ToString(),
                    Description = s["Description"].ToString()
                }).ToList();

            data.RequestTypes.ForEachAsync(t => t.IsAvailable = false).Wait();
            data.SaveChanges();
            
            ;
            
            foreach (var type in typesListFromJson)
            {
                var typeFromDB = data.RequestTypes.FirstOrDefault(t => t.Name == type.Name);
                if (typeFromDB == null)
                {
                    Create(type.Name, type.Description);
                    typeFromDB = data.RequestTypes.FirstOrDefault(t => t.Name == type.Name);
                }
                else if (typeFromDB.Description != type.Description)
                {
                    typeFromDB.Description = type.Description;
                }
                typeFromDB.IsAvailable = true;
            }
            
            data.SaveChanges();
        }

        public void Create(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Request type name can not be null or empty. ");
            }
            if (name.Length > DataValidation.RequestTypeNameMaxLenght)
            {
                throw new InvalidOperationException($"Request type name can not be longer then {DataValidation.RequestTypeNameMaxLenght} characters. ");
            }
            if (data.RequestTypes.Any(rt => rt.Name.ToLower() == name.ToLower()))
            {
                throw new InvalidOperationException($"Request type: {name}, already exist. ");
            }
            var requestType = new RequestType
            {
                Name = name,
                Description = description,
                IsDeleted = false,
                IsAvailable = true
            };

            data.RequestTypes.Add(requestType);
            data.SaveChanges();
        }

        public void Delete(int requestTypeID)
        {
            var requestType = data.RequestTypes.FirstOrDefault(r => r.ID == requestTypeID);
            requestType.IsDeleted = true;
            data.SaveChanges();
        }

        public Models.Request.RequestTypeServiceModel FindTypeByName(string typeName)
        {

            var type = data.RequestTypes.FirstOrDefault(t => t.Name == typeName);

            if (type == null)
            {
                Create(typeName, "No info. Created dinamic");
                type = data.RequestTypes.FirstOrDefault(t => t.Name == typeName);
            }

            return new Models.Request.RequestTypeServiceModel()
            {
                ID = type.ID,
                Name = type.Name,
                Description = type.Description
            };

        }
    }
}
