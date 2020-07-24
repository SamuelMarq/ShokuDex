using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Data.UserInfo;

namespace Recodme.ShokuDex.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> UserManager { get; set; }
        private SignInManager<User> SignInManager { get; set; }
        private RoleManager<Role> RoleManager { get; set; }


        public IActionResult Index()
        {
            return View();
        }
    }
}