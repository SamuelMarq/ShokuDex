using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;
using WebApi.Models;

namespace Recodme.ShokuDex.WebApi.Controllers.Web.FoodInfo
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class FoodController : Controller
    {
        private readonly FoodsBusinessObject _fbo = new FoodsBusinessObject();
        private readonly CategoriesBusinessObject _cbo = new CategoriesBusinessObject();
        

        public async Task<IActionResult> Index()
        {
            var listOperation = await _fbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var dic = new Dictionary<FoodViewModel,string>();
            foreach (var item in listOperation.Result)
            {
                var cat = await _cbo.ReadAsync(item.CategoryId);
                var catName = cat.Result.Name;
                dic.Add(FoodViewModel.Parse(item), catName);
            }
            return View(dic);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _fbo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var cat = await _cbo.ReadAsync(getOperation.Result.CategoryId);
            var catName = cat.Result.Name;
            var vm = FoodViewModel.Parse(getOperation.Result);
            var dic = new Dictionary<FoodViewModel, string>();
            dic.Add( vm, catName );
            return View(dic);
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
