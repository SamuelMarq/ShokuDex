using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.Api.FoodInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimesOfDayController : ControllerBase
    {
        private readonly TimesOfDayBusinessObject _bo = new TimesOfDayBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]TimeOfDayViewModel todvm)
        {
            var tod = todvm.ToTimeOfDay();
            var res = _bo.Create(tod);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<TimeOfDayViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var todvm = TimeOfDayViewModel.Parse(res.Result);

                return todvm;
            }
            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<TimeOfDayViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<TimeOfDayViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(TimeOfDayViewModel.Parse(item));
            }

            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] TimeOfDayViewModel todvm)
        {
            var currentRes = _bo.Read(todvm.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (SupportMethods.SupportMethods.Equals(todvm, current)) return base.StatusCode((int)HttpStatusCode.NotModified);

            current = SupportMethods.SupportMethods.Update(todvm, current);

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