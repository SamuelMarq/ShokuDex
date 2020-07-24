using Microsoft.AspNetCore.Identity;
using Recodme.ShokuDex.Data.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recodme.ShokuDex.Data.UserInfo
{
    public class Profiles : DescribedEntity
    {
        private string _gender;
        [Required(ErrorMessage = "Insert gender")]
        public string Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                RegisterChange();
            }
        }

        private double _height;
        [Required(ErrorMessage = "Insert height")]
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                RegisterChange();
            }
        }

        private DateTime _birthDate;
        [Required(ErrorMessage = "Insert birthdate")]
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                RegisterChange();
            }
        }

        private string _email;
        [Required(ErrorMessage = "Insert email")]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RegisterChange();
            }
        }


        private string _photo;
        public string Photo
        {
            get => _photo;
            set
            {
                _photo = value;
                RegisterChange();
            }
        }


        private string _license;
        public string License
        {
            get => _license;
            set
            {
                _license = value;
                RegisterChange();
            }
        }

        private int _numberOfPatients;
        public int NumberOfPatients
        {
            get => _numberOfPatients;
            set
            {
                _numberOfPatients = value;
                RegisterChange();
            }
        }




        public Profiles(string name, string description, string gender, double height, DateTime birthDate, string email, string photo, string license, int numberOfPatients) : base(name, description)
        {
            _gender = gender;
            _height = height;
            _birthDate = birthDate;
            _email = email;
            _photo = photo;
            _license = license;
            _numberOfPatients = numberOfPatients;
        }

        public Profiles(Guid id, DateTime createdAt, DateTime updatedAd, bool isDeleted, string name, string description, string gender, double height, DateTime birthDate, string email, string photo, string license, int numberOfPatients) : base(id, createdAt, updatedAd, isDeleted, name, description)
        {
            _gender = gender;
            _height = height;
            _birthDate = birthDate;
            _email = email;
            _photo = photo;
            _license = license;
            _numberOfPatients = numberOfPatients;
        }

        public Profiles(string name, string description, string gender, double height, DateTime birthDate, string email, string photo, string license) : base(name, description)
        {
            _gender = gender;
            _height = height;
            _birthDate = birthDate;
            _email = email;
            _photo = photo;
            _license = license;
        }
    }
}
