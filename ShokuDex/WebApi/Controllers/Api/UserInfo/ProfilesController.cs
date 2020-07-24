using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.WebApi.Models.UserInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.Api.UserInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesBusinessObject _bo = new ProfilesBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]ProfileViewModel pvm)
        {
            var p = pvm.ToProfile();
            var res = _bo.Create(p);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<ProfileViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var pvm = ProfileViewModel.Parse(res.Result);

                return pvm;
            }
            else return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        public ActionResult<List<ProfileViewModel>> List()
        {
            var res = _bo.List();
            if (!res.Success) StatusCode((int)HttpStatusCode.InternalServerError);
            var list = new List<ProfileViewModel>();
            foreach (var item in res.Result)
            {
                list.Add(ProfileViewModel.Parse(item));
            }

            return list;
        }

        [HttpPut]
        public ActionResult Update([FromBody] ProfileViewModel pvm)
        {
            var currentRes = _bo.Read(pvm.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (SupportMethods.SupportMethods.Equals(pvm, current)) return base.StatusCode((int)HttpStatusCode.NotModified);

            current = SupportMethods.SupportMethods.Update(pvm, current);

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