using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.WebApi.SupportMethods;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.Api.FoodInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodsBusinessObject _bo = new FoodsBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]FoodViewModel fvm)
        {
            var f = fvm.ToFood();
            var res = _bo.Create(f);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<FoodViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var fvm = FoodViewModel.Parse(res.Result);
                return fvm;
            }
            else return StatusCode((int)HttpStatusCode.InternalServerError);;
        }

        [HttpGet]
        public ActionResult<List<FoodViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<FoodViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(FoodViewModel.Parse(item));
            }

            return list;
        }

/*        [HttpGet("Find")]
        public ActionResult<List<FoodViewModel>> Find(string searchFood, Guid id=default)
        {
            var res = _bo.Find(searchFood, id);
            if (!res.Success) StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<FoodViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(FoodViewModel.Parse(item));
            }

            return list;
        }*/

        [HttpPut]
        public ActionResult Update([FromBody] FoodViewModel fvm)
        {
            var currentRes = _bo.Read(fvm.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (SupportMethods.SupportMethods.Equals(fvm, current)) return base.StatusCode((int)HttpStatusCode.NotModified);

            current = SupportMethods.SupportMethods.Update(fvm, current);
      

            var updateResult = _bo.Update(current);
            if (!updateResult.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var result = _bo.Delete(id);
            if (result.Success) return Ok();
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }


    }
}