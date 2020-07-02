using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.WebApi.Models.FoodInfo;

namespace Recodme.ShokuDex.WebApi.Controllers.FoodInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodsBusinessObject _bo = new FoodsBusinessObject();

        [HttpPost]
        public ActionResult Create([FromBody]FoodViewModel vm)
        {
            var f = vm.ToFood();
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
                var fvm = new FoodViewModel();
                fvm.Id = res.Result.Id;
                fvm.Name = res.Result.Name;
                fvm.Description = res.Result.Description;
                fvm.Fats = res.Result.Fats;
                fvm.Carbohydrates = res.Result.Carbohydrates;
                fvm.Protein = res.Result.Protein;
                fvm.Calories = res.Result.Calories;
                fvm.Alcohol = res.Result.Alcohol;
                fvm.Calories = res.Result.Calories;
                fvm.Portion = res.Result.Portion;
                fvm.Photo = res.Result.Photo;
                fvm.IsRecipe = res.Result.IsRecipe;
                fvm.ProfileId = res.Result.ProfileId;
                fvm.CategoryId = res.Result.CategoriesId;
                return fvm;
            }
            else return StatusCode((int)HttpStatusCode.InternalServerError);
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

        [HttpPut]
        public ActionResult Update([FromBody] FoodViewModel f)
        {
            var currentRes = _bo.Read(f.Id);
            if (!currentRes.Success) return StatusCode((int)HttpStatusCode.InternalServerError);
            var current = currentRes.Result;
            if (current == null) return NotFound();

            if (current.Name == f.Name && current.Description == f.Description &&
                current.Fats == f.Fats && current.Carbohydrates == f.Carbohydrates && current.Protein == f.Protein 
                && current.Alcohol == f.Alcohol && current.Calories == f.Calories && current.Portion == f.Portion &&
                current.Photo == f.Photo && current.IsRecipe == f.IsRecipe && current.ProfileId == f.ProfileId &&
                current.CategoriesId == f.CategoryId) return StatusCode((int)HttpStatusCode.NotModified);


            if (current.Name != f.Name) current.Name = f.Name;
            if (current.Description != f.Description) current.Description = f.Description;
            if (current.Fats != f.Fats) current.Fats = f.Fats;
            if (current.Carbohydrates != f.Carbohydrates) current.Carbohydrates = f.Carbohydrates;
            if (current.Protein != f.Protein) current.Protein = f.Protein;
            if (current.Alcohol != f.Alcohol) current.Alcohol = f.Alcohol;
            if (current.Calories != f.Calories) current.Calories = f.Calories;
            if (current.Protein != f.Protein) current.Protein = f.Protein;
            if (current.Photo != f.Photo) current.Photo = f.Photo;
            if (current.IsRecipe != f.IsRecipe) current.IsRecipe = f.IsRecipe;
            if (current.ProfileId != f.ProfileId) current.ProfileId = f.ProfileId;
            if (current.CategoriesId != f.CategoryId) current.CategoriesId= f.CategoryId;

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