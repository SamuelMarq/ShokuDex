﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.Data.FoodInfo;
using System;

namespace Recodme.ShokuDex.TestLibrary
{
    [TestClass]
    public class Build2Tests
    {
        private readonly MealsBusinessObject _bo = new MealsBusinessObject();

        [TestMethod]
        public void ListMeal()
        {
            var res = _bo.ListAsync();
            var food = res.Result;

            Assert.IsTrue(res.IsCompletedSuccessfully);
        }
    }
}
