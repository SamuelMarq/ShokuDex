using Recodme.ShokuDex.Data.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Recodme.ShokuDex.Data.UserInfo
{
    public class Profiles : DescribedEntity
    {
        





        public Profiles(string name, string description) : base(name, description)
        {

        }

        public Profiles(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name, string description) : base(id, createdAt, updatedAd, isDeleted, name, description)
        {

        }
    }
}
