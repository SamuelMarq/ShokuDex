using Recodme.ShokuDex.DataAccess.Contexts;
using System;

namespace Recodme.ShokuDex.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new FoodLogContext();
            ctx.Database.EnsureCreated();


        }
    }
}
