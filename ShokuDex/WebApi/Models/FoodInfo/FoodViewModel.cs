using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Recodme.ShokuDex.Data.FoodInfo;
using System;
using System.Linq;
using System.Reflection;

namespace Recodme.ShokuDex.WebApi.Models.FoodInfo
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public double Protein { get; set; }
        public double Alcohol { get; set; }
        public double Calories { get; set; }
        public double Portion { get; set; }
        public string Photo { get; set; }
        public bool IsRecipe { get; set; }
        public Guid ProfileId { get; set; }
        public Guid CategoryId { get; set; }


        public Foods ToFood()
        {
            return new Foods(Name, Description, Fats, Carbohydrates, Protein, Alcohol, Calories, Portion, Photo, IsRecipe, ProfileId, CategoryId);
        }

        public static FoodViewModel Parse(Foods obj)
        {
            return new FoodViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Fats = obj.Fats,
                Carbohydrates = obj.Carbohydrates,
                Protein = obj.Protein,
                Alcohol = obj.Alcohol,
                Calories = obj.Calories,
                Portion = obj.Portion,
                Photo = obj.Photo,
                IsRecipe = obj.IsRecipe,
                ProfileId = obj.ProfileId,
                CategoryId = obj.CategoryId
            };
        }
    }
}