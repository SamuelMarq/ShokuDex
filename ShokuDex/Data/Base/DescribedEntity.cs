using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.ShokuDex.Data.Base
{
    public abstract class DescribedEntity : NamedEntity
    {
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RegisterChange();
            }
        }

        public DescribedEntity(string name, string description) : base(name)
        {
            _description = description;
        }

        public DescribedEntity(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name, string description) : base(id, createdAt, updatedAd, isDeleted, name)
        {
            _description = description;
        }
    }
}
