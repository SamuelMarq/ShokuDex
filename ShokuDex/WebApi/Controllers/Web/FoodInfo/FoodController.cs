using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.Web.FoodInfo
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly FoodsBusinessObject _bo = new FoodsBusinessObject();
        

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")]FoodViewModel vm)
        {
            if (ModelState.IsValid)
            {

            }
            return RedirectToAction("Index");
        }



    }
}
