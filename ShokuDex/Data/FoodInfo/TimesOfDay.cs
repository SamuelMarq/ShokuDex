using Recodme.ShokuDex.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.ShokuDex.Data.FoodInfo
{
    public class TimesOfDay : NamedEntity
    {



        public virtual ICollection<Meals> Meals { get; set; }


        public TimesOfDay(string name) : base(name)
        {

        }

        public TimesOfDay(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name) : base(id, createdAt, updatedAd, isDeleted, name)
        {

        }
    }
}
