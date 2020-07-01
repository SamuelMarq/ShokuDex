using Recodme.ShokuDex.Data.FoodInfo;
using System;

namespace Recodme.ShokuDex.WebApi.Models.FoodInfo
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Categories ToCategory()
        {
            return new Categories(Id, DateTime.UtcNow, DateTime.UtcNow, false, Name);
        }

        public static CategoryViewModel Parse(Categories obj)
        {
            return new CategoryViewModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public bool Equals(Categories obj)
        {
            bool res = (Id == obj.Id);
            if (res) res = (Name == obj.Name);
            return res;
        }
    }
}