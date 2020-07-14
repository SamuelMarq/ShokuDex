using Recodme.ShokuDex.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.ShokuDex.Data.FoodInfo
{
    public class Meals : Entity
    {
        private DateTime _date;
        [Required(ErrorMessage = "Insert date")]
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                RegisterChange();
            }
        }

        private string _portion;
        [Required(ErrorMessage = "Insert portion")]
        public string Portion
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


        public Meals(DateTime date, string portion, bool isSugestion, Guid profileId, Guid foodId, Guid timeOfDayId) : base()
        {
            _date = date;
            _portion = portion;
            _isSugestion = isSugestion;
            ProfileId = profileId;
            FoodId = foodId;
            TimeOfDayId = timeOfDayId;
        }

        public Meals(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, DateTime date, string portion, bool isSugestion, Guid profileId, Guid foodId, Guid timeOfDayId) : base(id, createdAt, updatedAd, isDeleted)
        {
            _date = date;
            _portion = portion;
            _isSugestion = isSugestion;
            ProfileId = profileId;
            FoodId = foodId;
            TimeOfDayId = timeOfDayId;
        }
    }
}
