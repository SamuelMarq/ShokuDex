using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.Data.FoodInfo;
using System;

namespace Recodme.ShokuDex.TestLibrary
{
    [TestClass]
    public class Build1Tests
    {

        private readonly FoodsBusinessObject _bo = new FoodsBusinessObject();


        [TestMethod]
        public void AddFood()
        {
                var batata = new Foods("Batata", "tuberculo amarelo", 2934720, 23, 1, 10000000, 6, 123124, "", false, Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("fb56fd90-822b-4be9-09b6-08d822831157"));
                  var res = _bo.Create(batata);
        
                Assert.IsTrue(res.Success);
        }

        [TestMethod]
        public void ViewFood()
        {
            var res = _bo.Read(Guid.Parse("5ABA9C14-D64E-42DC-BD8C-08D8228AFBF6"));
            var food = res.Result;
            bool isEqual = food.Alcohol == 1 && food.Calories == 24 && food.Carbohydrates == 1 && food.Fats == 1 && food.Portion == 1 && food.Protein == 1 && food.Name == "arroz doce" && food.Photo == "arroz" && food.IsRecipe == false && food.Description == "arroz";

            Assert.IsTrue(isEqual);
        }

        [TestMethod]
        public void UpdateFood()
        {
            var batata = _bo.Read(Guid.Parse("5ABA9C14-D64E-42DC-BD8C-08D8228AFBF6"));
            var res = _bo.Update(batata.Result);
            bool isEqual = res.Equals(batata);

            Assert.IsTrue(!isEqual);
        }

        [TestMethod]
        public void DeleteFood()
        {
            var res = _bo.Delete(Guid.Parse("5ABA9C14-D64E-42DC-BD8C-08D8228AFBF6"));

            Assert.IsTrue(res.Success);
        }

        [TestMethod]
        public void AddFoodIncomplete()
        {
            var f = new Foods("", "", 2934720, 23, 1, 10000000, 6, 123124, "", false, Guid.Parse("00000000-0000-0000-0000-000000000000"), Guid.Parse("fb56fd90-822b-4be9-09b6-08d822831157"));

            var res = _bo.Create(f);

            Assert.IsFalse(res.Success);
        }

        [TestMethod]
        public void FindFood()
        {
            var res = _bo.Find("a");
            Assert.IsTrue(res.Success);
        }

        [TestMethod]
        public void FindFoodByCat()
        {
            var res = _bo.Find("", Guid.Parse("FB56FD90-822B-4BE9-09B6-08D822831157"));
            Assert.IsTrue(res.Success);
        }
    }
}
