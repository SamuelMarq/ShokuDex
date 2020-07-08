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
            bool isEqual = res.Result.Alcohol == 1 && res.Result.Calories == 24 && res.Result.Carbohydrates == 1 && res.Result.Fats == 1 && res.Result.Portion == 1 && res.Result.Protein == 1 && res.Result.Name == "arroz doce" && res.Result.Photo == "arroz" && res.Result.IsDeleted == false;

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
