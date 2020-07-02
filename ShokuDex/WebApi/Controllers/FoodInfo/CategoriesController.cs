using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoDAO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.FoodInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesBusinessObject _bo = new CategoriesBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]CategoryViewModel vm)
        {
            var cc = vm.ToCategory();
            var res = _bo.Create(cc);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var ccvm = new CategoryViewModel();
                ccvm.Id = res.Result.Id;
                ccvm.Name = res.Result.Name;
               
                return ccvm;
            }
            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<CategoryViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<CategoryViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(CategoryViewModel.Parse(item));
            }

            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] CategoryViewModel cc)
        {
            var currentRes = _bo.Read(cc.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name== cc.Name) return StatusCode((int)HttpStatusCode.NotModified);


            if (current.Name != cc.Name) current.Name = cc.Name;
            

            var updateResult = _bo.Update(current);
            if (!updateResult.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var result = _bo.Delete(id);
            if (result.Success) return Ok();
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}