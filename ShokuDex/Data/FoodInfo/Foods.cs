using Recodme.ShokuDex.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.ShokuDex.Data.FoodInfo
{
    public class Foods : DescribedEntity
    {
        private double _fats;
        [Required(ErrorMessage = "Insert fats")]
        public double Fats
        {
            get => _fats;
            set
            {
                _fats = value;
                RegisterChange();
            }
        }

        private double _carbohydrates;
        [Required(ErrorMessage = "Insert carbohydrates")]
        public double Carbohydrates
        {
            get => _carbohydrates;
            set
            {
                _carbohydrates = value;
                RegisterChange();
            }
        }

        private double _protein;
        [Required(ErrorMessage = "Insert protein")]
        public double Protein
        {
            get => _protein;
            set
            {
                _protein = value;
                RegisterChange();
            }
        }

        private double _alcohol;
        [Required(ErrorMessage = "Insert alcohol")]
        public double Alcohol
        {
            get => _alcohol;
            set
            {
                _alcohol = value;
                RegisterChange();
            }
        }

        private double _calories;
        [Required(ErrorMessage = "Insert calories")]
        public double Calories
        {
            get => _calories;
            set
            {
                _calories = value;
                RegisterChange();
            }
        }

        private double _portion;
        [Required(ErrorMessage = "Insert portion")]
        public double Portion
        {
            get => _portion;
            set
            {
                _portion = value;
                RegisterChange();
            }
        }

        private string _photo;
        public string Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                RegisterChange();
            }
        }

        private bool _isRecipe;
        [Display(Name = "Is a recipe")]
        [Required(ErrorMessage = "Confirm if it is a recipe")]
        public bool IsRecipe
        {
            get => _isRecipe;
            set
            {
                _isRecipe = value;
                RegisterChange();
            }
        }

        public Guid ProfileId { get; set; }

        [ForeignKey("Categories")]
        public Guid CategoriesId { get; set; }
        

        public virtual Categories Categories { get; set; }


        public Foods(string name, string description, double fats, double carbohydrates, double protein, double alcohol, 
                    double calories, double portion, string photo, bool isRecipe, Guid profileId, Guid categoriesId) : base(name, description)
        {
            _fats = fats;
            _carbohydrates = carbohydrates;
            _protein = protein;
            _alcohol = alcohol;
            _calories = calories;
            _portion = portion;
            _photo = photo;
            _isRecipe = isRecipe;
            ProfileId = profileId;
            CategoriesId = categoriesId;
        }

        public Foods(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name, string description, 
                    double fats, double carbohydrates, double protein, double alcohol, double calories, double portion, 
                    string photo, bool isRecipe, Guid profileId, Guid categoriesId) : base(id, createdAt, updatedAd, isDeleted, name, description)
        {
            _fats = fats;
            _carbohydrates = carbohydrates;
            _protein = protein;
            _alcohol = alcohol;
            _calories = calories;
            _portion = portion;
            _photo = photo;
            _isRecipe = isRecipe;
            ProfileId = profileId;
            CategoriesId = categoriesId;
        }
    }
}
