using FMS.Data;
using FMS.Data.Models.Employee;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace FMS.Services.Implementations
{
    public class GenderService : IGenderService
    {
        private readonly FMSDBContext data;

        public GenderService(FMSDBContext data)
            => this.data = data;
        public void Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidOperationException("Gender name can not be null or empty. ");
            }
            if (data.Genders.Any(g => g.Name == name))
            {
                throw new InvalidOperationException("Gender alredy exist. ");
            }
            var gender = new Gender
            {
                Name = name
            };
            this.data.Genders.Add(gender);
            this.data.SaveChanges();
        }

        public Gender GetDefaultGender()
        {
            var defaultGender = data.Genders.Any(g => g.Name == "Undefined");
            if (!defaultGender)
            {
                data.Genders.Add(new Gender()
                {
                    Name = "Undefined"
                });
                data.SaveChanges();
            }
            return data.Genders.FirstOrDefault(g => g.Name == "Undefined");
        }
    }
}
