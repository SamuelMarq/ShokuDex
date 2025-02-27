﻿using System;
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
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesBusinessObject _bo = new CategoriesBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]CategoryViewModel cvm)
        {
            var c = cvm.ToCategory();
            var res = _bo.Create(c);
            return StatusCode(res.Success ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryViewModel> Get(Guid id)
        {
            var res = _bo.Read(id);
            if (res.Success)
            {
                if (res.Result == null) return NotFound();
                var cvm = CategoryViewModel.Parse(res.Result);
               
                return cvm;
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
        public ActionResult Update([FromBody] CategoryViewModel cvm)
        {
            var currentRes = _bo.Read(cvm.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (SupportMethods.SupportMethods.Equals(cvm, current)) return base.StatusCode((int)HttpStatusCode.NotModified);

            current = SupportMethods.SupportMethods.Update(cvm, current);

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