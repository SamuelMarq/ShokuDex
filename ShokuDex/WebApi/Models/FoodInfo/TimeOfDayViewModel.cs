using Recodme.ShokuDex.Data.FoodInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.ShokuDex.WebApi.Models.FoodInfo
{
    public class TimeOfDayViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public TimesOfDay ToTimeOfDay()
        {
            return new TimesOfDay(Name);
        }

        public static TimeOfDayViewModel Parse(TimesOfDay obj)
        {
            return new TimeOfDayViewModel()
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }
    }
}
