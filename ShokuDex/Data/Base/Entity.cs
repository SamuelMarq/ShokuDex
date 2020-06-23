using System;
using System.ComponentModel.DataAnnotations;

namespace Recodme.ShokuDex.Data.Base
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; private set; }

        [Display(Name = "Created At")]
        [Required]
        public DateTime CreatedAt { get; private set; }

        [Display(Name = "Updated At")]
        [Required]
        public DateTime UpdatedAt { get; protected set; }

        private bool _isDeleted;
        [Display(Name = "Deleted")]
        [Required]
        public bool IsDeleted
        {
            get => _isDeleted;
            set
            {
                _isDeleted = value;
                RegisterChange();
            }
        }

        protected virtual void RegisterChange()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
            _isDeleted = false;
        }

        protected Entity(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAd;
            _isDeleted = isDeleted;
        }
    }
}