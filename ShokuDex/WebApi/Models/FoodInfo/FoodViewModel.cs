using Recodme.ShokuDex.Data.FoodInfo;
using System;

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
            return new Foods(Id, DateTime.UtcNow, DateTime.UtcNow, false, Name, Description, Fats, Carbohydrates, Protein, Alcohol, Calories, Portion, Photo, IsRecipe, ProfileId, CategoryId);
        }

        public static FoodViewModel Parse(Foods obj)
        {
            return new FoodViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Fats = obj.Fats,
                Carbohydrates=obj.Carbohydrates,
                Protein=obj.Protein,
                Alcohol=obj.Alcohol,
                Calories=obj.Calories,
                Portion=obj.Portion,
                Photo=obj.Photo,
                IsRecipe=obj.IsRecipe,
                ProfileId=obj.ProfileId,
                CategoryId=obj.CategoriesId
            };
        }

        public bool Equals(Foods obj)
        {
            bool res = (Id == obj.Id);
            if (res) res = (Name == obj.Name); else return false;
            if (res) res = (Description == obj.Description); else return false;
            if (res) res = (Fats == obj.Fats); else return false;
            if (res) res = (Carbohydrates == obj.Carbohydrates); else return false;
            if (res) res = (Protein == obj.Protein); else return false;
            if (res) res = (Alcohol == obj.Alcohol); else return false;
            if (res) res = (Portion == obj.Portion); else return false;
            if (res) res = (Photo == obj.Photo); else return false;
            if (res) res = (IsRecipe == obj.IsRecipe); else return false;
            if (res) res = (ProfileId == obj.ProfileId); else return false;
            if (res) res = (CategoryId == obj.CategoriesId);
            return res;
        }
    }
}