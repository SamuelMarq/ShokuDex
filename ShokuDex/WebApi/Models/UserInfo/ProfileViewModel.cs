using Recodme.ShokuDex.Data.UserInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.ShokuDex.WebApi.Models.UserInfo
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Input the email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Input the gender")]
        public string Gender { get; set; }

        [Display(Name = "Height")]
        //[Required(ErrorMessage = "Input the height")]
        public double Height { get; set; }

        [Display(Name = "BirthDate")]
        [Required(ErrorMessage = "Input the birthdate")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Display(Name = "License")]
        public string License { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Input the name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public Profiles ToProfile()
        {
            return new Profiles(Name, Description, Gender, Height, BirthDate, Email, Photo, License);
        }

        public static ProfileViewModel Parse(Profiles obj)
        {
            return new ProfileViewModel()
            {
                Id = obj.Id,
                Name = obj.Name,
                Description = obj.Description,
                Gender = obj.Gender,
                Height = obj.Height,
                BirthDate = obj.BirthDate,
                Email = obj.Email,
                Photo = obj.Photo,
                License = obj.License, 
            };
        }
    }
}
