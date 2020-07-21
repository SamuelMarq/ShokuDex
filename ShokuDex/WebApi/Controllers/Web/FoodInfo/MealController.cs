using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoDAO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;
using WebApi.Models;

namespace Recodme.ShokuDex.WebApi.Controllers.Web.FoodInfo
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MealController : Controller
    {
        private readonly MealsBusinessObject _bo = new MealsBusinessObject();
        private readonly ProfilesBusinessObject _pbo = new ProfilesBusinessObject();
        private readonly FoodsBusinessObject _fbo = new FoodsBusinessObject();
        private readonly TimesOfDayBusinessObject _todbo = new TimesOfDayBusinessObject();


        private async Task<List<FoodViewModel>> GetFoodViewModels(List<Guid> ids)
        {
            var filterOperation = await _fbo.FilterAsync(x => ids.Contains(x.Id));
            var fList = new List<FoodViewModel>();
            foreach (var item in filterOperation.Result)
            {
                fList.Add(FoodViewModel.Parse(item));
            }
            return fList;
        }

        private async Task<FoodViewModel> GetFoodViewModel(Guid id)
        {
            var getOperation = await _fbo.ReadAsync(id);
            return FoodViewModel.Parse(getOperation.Result);
        }

        private async Task<List<TimeOfDayViewModel>> GetTimeOfDayViewModels()
        {
            var filterOperation = await _todbo.ListAsync();
            var todList = new List<TimeOfDayViewModel>();
            foreach (var item in filterOperation.Result)
            {
                todList.Add(TimeOfDayViewModel.Parse(item));
            }
            return todList;
        }

        private async Task<TimeOfDayViewModel> GetTimeOfDayViewModel(Guid id)
        {
            var getOperation = await _todbo.ReadAsync(id);
            return TimeOfDayViewModel.Parse(getOperation.Result);
        }

        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var fIds = listOperation.Result.Select(x => x.FoodId).Distinct().ToList();
            var todIds = listOperation.Result.Select(x => x.TimeOfDayId).Distinct().ToList();
            var lst = new List<MealViewModel>();
            foreach (var item in listOperation.Result)
            {
                lst.Add(MealViewModel.Parse(item));
            }
            ViewData["Title"] = "Menus";
            ViewData["BreadCrumbs"] = new List<string>() { "Home", "Menus" };
            ViewData["Food"] = await GetFoodViewModels(fIds);
            ViewData["TimeOfDay"] = await GetTimeOfDayViewModels();
            return View(lst);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = MealViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] MealViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var m = vm.ToMeal();
                var createOperation = await _bo.CreateAsync(m);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = MealViewModel.Parse(getOperation.Result);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id, Name")] MealViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (id == null) return NotFound();

                var getOperation = await _bo.ReadAsync((Guid)id);
                if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
                if (getOperation.Result == null) return NotFound();

                if (!SupportMethods.SupportMethods.Equals(vm, getOperation.Result))
                {
                    var current = SupportMethods.SupportMethods.Update(vm, getOperation.Result);
                    var updateOperation = await _bo.UpdateAsync(current);
                    if (!updateOperation.Success) return base.View("Error", new ErrorViewModel() { RequestId = updateOperation.Exception.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}