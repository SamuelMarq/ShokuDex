using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.WebApi.Models.UserInfo;
using WebApi.Models;

namespace Recodme.ShokuDex.WebApi.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<User> UserManager { get; set; }
        private SignInManager<User> SignInManager { get; set; }
        private RoleManager<ShokuDexRole> RoleManager { get; set; }

        private readonly ProfilesBusinessObject _pbo = new ProfilesBusinessObject();


        public AccountController(UserManager<User> uManager, SignInManager<User> sManager, RoleManager<ShokuDexRole> rManager)
        {
            UserManager = uManager;
            SignInManager = sManager;
            RoleManager = rManager;
        }

        [AllowAnonymous]
        [HttpPost("/GenerateToken")]
        public IActionResult GenerateToken(LoginViewModel vm)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var accountBo = new AccountBusinessObject(UserManager, RoleManager);
            var profile = new Profiles(vm.Name, vm.Description, vm.Gender, vm.Height, vm.BirthDate, vm.Email, vm.Photo, vm.License, 0);
            //var profileCreation = await _pbo.CreateAsync(profile);
            //if (!profileCreation.Success) return View("Error", new ErrorViewModel() { RequestId = profileCreation.Exception.Message });
            var registerOperation = await accountBo.Register(vm.UserName, vm.Password, profile, "User");
            return RedirectToAction(nameof(Index), "Home");
        }


        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var loginOperation = await SignInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (!loginOperation.Succeeded) ViewData["Res"] = "Login Failed";
            return View(vm);
        }


        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}