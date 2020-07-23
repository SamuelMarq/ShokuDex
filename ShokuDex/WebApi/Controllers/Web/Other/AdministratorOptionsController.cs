using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Recodme.ShokuDex.WebApi.Controllers.Web.Other
{
    public class AdministratorOptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
