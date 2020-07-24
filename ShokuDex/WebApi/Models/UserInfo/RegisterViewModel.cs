using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recodme.ShokuDex.WebApi.Models.UserInfo
{
    public class RegisterViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Input the user name")]
        public string UserName { get; set; }

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

        [Required(ErrorMessage = "Input the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
