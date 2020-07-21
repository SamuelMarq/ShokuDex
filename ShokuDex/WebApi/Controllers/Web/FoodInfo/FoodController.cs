using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //var listOperation = await _fbo.FilterAsync(x => x.isGlobal==... || x.ProfileId == ...);
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
            var catOperation = await _cbo.ReadAsync(getOperation.Result.CategoryId);
            var fvm = FoodViewModel.Parse(getOperation.Result);
            var cvm = CategoryViewModel.Parse(catOperation.Result);
            var dic = new Dictionary<FoodViewModel, CategoryViewModel>();
            dic.Add( fvm, cvm);
            return View(dic);
        }

        public async Task<IActionResult> Create()
        {
            var listOperation = await _cbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var cats = new List<SelectListItem>();
            foreach (var item in listOperation.Result)
            {
                cats.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Categories = cats;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FoodViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var f = vm.ToFood();
                var createOperation = await _fbo.CreateAsync(f);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        /*public async Task<IActionResult> Edit()
        {
            var listOperation = await _cbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var cats = new List<SelectListItem>();
            foreach (var item in listOperation.Result)
            {
                cats.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Categories = cats;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FoodViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var f = vm.ToFood();
                var createOperation = await _fbo.CreateAsync(f);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }*/
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _fbo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }

    }
}
