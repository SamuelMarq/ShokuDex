using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Recodme.ShokuDex.Data.FoodInfo;
using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.ShokuDex.WebApi.Models.FoodInfo
{
    public class FoodViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insert Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [Required(ErrorMessage = "Insert amount of Fats")]
        public double Fats { get; set; }

        [Required(ErrorMessage = "Insert amount of Carbohydrates")]
        public double Carbohydrates { get; set; }

        [Required(ErrorMessage = "Insert amount of Protein")]
        public double Protein { get; set; }

        public double Alcohol { get; set; }

        public double Calories { get; set; }

        [Display(Name = "Recommended Portion")]
        public double Portion { get; set; }

        public string Photo { get; set; }
        public bool IsGlobal { get; set; }
        public Guid ProfileId { get; set; }

        [Required(ErrorMessage = "Choose a Category")]
        public Guid CategoryId { get; set; }


        public Foods ToFood()
        {
            return new Foods(Name, Description, Fats, Carbohydrates, Protein, Alcohol, Calories, Portion, Photo, IsGlobal, ProfileId, CategoryId);
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
                IsGlobal = obj.IsGlobal,
                ProfileId = obj.ProfileId,
                CategoryId = obj.CategoryId
            };
        }
    }
}