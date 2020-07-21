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


        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var flistOperation = await _fbo.ListAsync();
            if (!flistOperation.Success) return View("Error", new ErrorViewModel() { RequestId = flistOperation.Exception.Message });
            var todlistOperation = await _todbo.ListAsync();
            if (!todlistOperation.Success) return View("Error", new ErrorViewModel() { RequestId = flistOperation.Exception.Message });

            var meallist = new List<MealViewModel>();
            foreach (var item in listOperation.Result)
            {
                meallist.Add(MealViewModel.Parse(item));
            }

            var flist = new List<FoodViewModel>();
            foreach (var item in flistOperation.Result)
            {
                flist.Add(FoodViewModel.Parse(item));
            }

            var todlist = new List<TimeOfDayViewModel>();
            foreach (var item in todlistOperation.Result)
            {
                todlist.Add(TimeOfDayViewModel.Parse(item));
            }

            ViewBag.Food = flistOperation;
            ViewBag.TimeOfDay = todlistOperation;
            return View(meallist);
        }


    }
}