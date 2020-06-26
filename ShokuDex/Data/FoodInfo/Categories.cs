using Recodme.ShokuDex.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.ShokuDex.Data.FoodInfo
{
    public class Categories : NamedEntity
    {



        public virtual ICollection<Foods> Foods { get; set; }


        public Categories(string name) : base(name)
        {

        }

        public Categories(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name) : base(id, createdAt, updatedAd, isDeleted, name)
        {

        }
    }
}
