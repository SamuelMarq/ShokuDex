using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.Data.FoodInfo;
using System;

namespace TestLibrary
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddFood()
        {
            var _bo = new FoodsBusinessObject();
           
                var batata = new Foods("Batata", "tuberculo amarelo", 2934720, 23, 1, 10000000, 6, 123124, "", false, Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("fb56fd90-822b-4be9-09b6-08d822831157"));
                  var res = _bo.Create(batata);
        
                Assert.IsTrue(res.Success);
        }

        [TestMethod]

        public void AddFoodIncomplete()
        {
            var _bo = new FoodsBusinessObject();
            var f = new Foods("", "", 2934720, 23, 1, 10000000, 6, 123124, "", false, Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("fb56fd90-822b-4be9-09b6-08d822831157"));

            var res = _bo.Create(f);

            Assert.IsFalse(res.Success);
        }

        [TestMethod]
        public void ViewFood()
        {
            var _bo = new FoodsBusinessObject();
            var res = _bo.Read(Guid.Parse("5ABA9C14-D64E-42DC-BD8C-08D8228AFBF6"));
            var food = res.Result;
            bool isEqual = food.Alcohol == 1 && food.Calories == 24 && food.Carbohydrates == 1 && food.Fats == 1 && food.Portion == 1 && food.Protein == 1 && food.Name == "arroz doce" && food.Photo == "arroz" && food.IsRecipe == false && food.Description == "arroz";

            Assert.IsTrue(isEqual);
        }
        
    }
}
