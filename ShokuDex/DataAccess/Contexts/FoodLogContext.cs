using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.DataAccess.Properties;

namespace Recodme.ShokuDex.DataAccess.Contexts
{
    public class FoodLogContext : IdentityDbContext
    {
        public FoodLogContext() : base()
        {

        }

        public FoodLogContext(DbContextOptions<FoodLogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Resources.OnlineConnectionString);
            }
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Profile>().HasOne(x => x.User).WithOne(x => x.Profile);
        //    base.OnModelCreating(builder);
        //}

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
    }

}
