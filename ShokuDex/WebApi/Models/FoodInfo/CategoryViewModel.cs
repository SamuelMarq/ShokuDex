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
            return new Categories(Name);
        }

        public static CategoryViewModel Parse(Categories obj)
        {
            return new CategoryViewModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }
    }
}