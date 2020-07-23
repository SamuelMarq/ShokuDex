using Recodme.ShokuDex.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.ShokuDex.Data.FoodInfo
{
    public class Meals : Entity
    {
        private DateTime _day;
        [Required(ErrorMessage = "Insert day")]
        public DateTime Day
        {
            get => _day;
            set
            {
                _day = value;
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

        private bool _isSugestion;
        [Display(Name = "Is a sugestion")]
        public bool IsSugestion
        {
            get => _isSugestion;
            set
            {
                _isSugestion = value;
                RegisterChange();
            }
        }

        public Guid ProfileId { get; set; }

        [ForeignKey("Foods")]
        public Guid FoodId { get; set; }

        [ForeignKey("TimesOfDay")]
        public Guid TimeOfDayId { get; set; }


        public virtual Foods Foods { get; set; }

        public virtual TimesOfDay TimesOfDay { get; set; }


        public Meals(DateTime day, double portion, bool isSugestion, Guid profileId, Guid foodId, Guid timeOfDayId) : base()
        {
            _day = day;
            _portion = portion;
            _isSugestion = isSugestion;
            ProfileId = profileId;
            FoodId = foodId;
            TimeOfDayId = timeOfDayId;
        }

        public Meals(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, DateTime day, double portion, bool isSugestion, Guid profileId, Guid foodId, Guid timeOfDayId) : base(id, createdAt, updatedAd, isDeleted)
        {
            _day = day;
            _portion = portion;
            _isSugestion = isSugestion;
            ProfileId = profileId;
            FoodId = foodId;
            TimeOfDayId = timeOfDayId;
        }
    }
}
