using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.ShokuDex.Data.Base
{
    public abstract class NamedEntity : Entity
    {
        private string _name;
        [Required]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RegisterChange();
            }
        }

        protected NamedEntity(string name) : base()
        {
            _name = name;
        }

        protected NamedEntity(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name) : base(id, createdAt, updatedAd, isDeleted)
        {
            _name = name;
        }
    }
}
