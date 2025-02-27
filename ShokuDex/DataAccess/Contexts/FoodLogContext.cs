﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.ShokuDex.Data.FoodInfo;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.DataAccess.Properties;

namespace Recodme.ShokuDex.DataAccess.Contexts
{
    public class FoodLogContext : IdentityDbContext<User, ShokuDexRole, int>
    {
        private string _connectionString = string.Empty;

        public FoodLogContext() : base()
        {

        }

        public FoodLogContext(string cString)
        {
            _connectionString = cString;
        }

        public FoodLogContext(DbContextOptions<FoodLogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (_connectionString == string.Empty)
                {
                    optionsBuilder.UseSqlServer(Resources.ConnectionString);
                }
                else
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Profiles>().HasOne(x => x.User).WithOne(x => x.Profile);
            base.OnModelCreating(builder);
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<TimesOfDay> TimesOfDay { get; set; }
        public DbSet<Meals> Meals { get; set; }
    }

}
