using Recodme.ShokuDex.Data.FoodInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.ShokuDex.WebApi.Models.FoodInfo
{
    public class MealViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Select the day")]
        public DateTime Day { get; set; }

        [Required(ErrorMessage = "Add the eaten portion")]
        public double Portion { get; set; }

        [Display(Name = "Is a sugestion")]
        public bool IsSugestion { get; set; }
        public Guid ProfileId { get; set; }

        [Display(Name = "Food")]
        [Required(ErrorMessage = "Select the food")]
        public Guid FoodId { get; set; }

        [Display(Name = "Time Of Day")]
        [Required(ErrorMessage = "Select the time of day")]
        public Guid TimeOfDayId { get; set; }

        public Meals ToMeal()
        {
            return new Meals(Day, Portion, IsSugestion, ProfileId, FoodId, TimeOfDayId);
        }

        public static MealViewModel Parse(Meals obj)
        {
            return new MealViewModel()
            {
                Id = obj.Id,
                Day = obj.Day,
                Portion = obj.Portion,
                IsSugestion = obj.IsSugestion,
                ProfileId = obj.ProfileId,
                FoodId = obj.FoodId,
                TimeOfDayId = obj.TimeOfDayId
            };
        }
    }
}
