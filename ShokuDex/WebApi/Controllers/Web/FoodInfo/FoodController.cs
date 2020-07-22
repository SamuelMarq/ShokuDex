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
    public class Food_TableController : Controller
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
                item.Fats = Math.Round(item.Fats, 2);
                item.Carbohydrates = Math.Round(item.Carbohydrates, 2);
                item.Protein = Math.Round(item.Protein, 2);
                item.Alcohol = Math.Round(item.Alcohol, 2);
                item.Calories = Math.Round(item.Calories, 2);
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

        public async Task<IActionResult> Insert_food()
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
        public async Task<IActionResult> Insert_food(FoodViewModel vm)
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

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _fbo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var fvm = FoodViewModel.Parse(getOperation.Result);

            var listOperation = await _cbo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var cats = new List<SelectListItem>();
            foreach (var item in listOperation.Result)
            {
                cats.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            ViewBag.Categories = cats;
            return View(fvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FoodViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if(id == null) return NotFound();

                var getOperation = await _fbo.ReadAsync((Guid)id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();

                vm.Calories = vm.Fats * 9 + vm.Carbohydrates * 4 + vm.Protein * 4 + vm.Alcohol * 7;

                if (!SupportMethods.SupportMethods.Equals(vm, getOperation.Result))
                {
                    var current = SupportMethods.SupportMethods.Update(vm, getOperation.Result);
                    var updateOperation = await _fbo.UpdateAsync(current);
                    if (!updateOperation.Success) return base.View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _fbo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }

    }
}
