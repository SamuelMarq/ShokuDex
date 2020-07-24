using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.WebApi.Models.UserInfo;
using WebApi.Models;

namespace Recodme.ShokuDex.WebApi.Controllers.Web.UserInfo
{
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ProfileController : Controller
    {
        private readonly ProfilesBusinessObject _bo = new ProfilesBusinessObject();


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOperation = await _bo.ListAsync();
            if (!listOperation.Success) return View("Error", new ErrorViewModel() { RequestId = listOperation.Exception.Message });
            var list = new List<ProfileViewModel>();
            foreach (var item in listOperation.Result)
            {
                list.Add(ProfileViewModel.Parse(item));
            }
            return View(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = ProfileViewModel.Parse(getOperation.Result);
            return View(vm);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("new")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var p = vm.ToProfile();
                var createOperation = await _bo.CreateAsync(p);
                if (!createOperation.Success) return View("Error", new ErrorViewModel() { RequestId = createOperation.Exception.Message });
                return RedirectToAction(nameof(Index));

            }
            return View(vm);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var getOperation = await _bo.ReadAsync((Guid)id);
            if (!getOperation.Success) return View("Error", new ErrorViewModel() { RequestId = getOperation.Exception.Message });
            if (getOperation.Result == null) return NotFound();
            var vm = ProfileViewModel.Parse(getOperation.Result);

            return View(vm);
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProfileViewModel vm)
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

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var deleteOperation = await _bo.DeleteAsync((Guid)id);
            if (!deleteOperation.Success) return View("Error", new ErrorViewModel() { RequestId = deleteOperation.Exception.Message });
            return RedirectToAction(nameof(Index));
        }
    }
}