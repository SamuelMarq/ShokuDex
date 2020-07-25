using Recodme.ShokuDex.Business.BusinessObjects.FoodInfoBO;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.DataAccess.Contexts;
using System;
using System.Text.RegularExpressions;

namespace Recodme.ShokuDex.App
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var ctx = new FoodLogContext();
            //ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            Console.WriteLine("Done!");
/*            FoodsBusinessObject _fbo = new FoodsBusinessObject();

            var regex = new Regex("^" + "", RegexOptions.IgnoreCase);
            var regex2 = new Regex("^" + "b", RegexOptions.IgnoreCase);
            var res2 = await _fbo.FilterAsync(x => regex.IsMatch(x.Name));
            var res3 = await _fbo.FilterAsync(x => regex2.IsMatch(x.Name));*/
    }
}
}
