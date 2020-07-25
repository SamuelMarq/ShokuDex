using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Recodme.ShokuDex.Business.BusinessObjects.UserInfoBO;
using Recodme.ShokuDex.Data.UserInfo;
using Recodme.ShokuDex.DataAccess.Contexts;
using Recodme.ShokuDex.WebApi.Options;

namespace Recodme.ShokuDex.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMemoryCache();
            //Politicas de cookies
            services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });

            //Definição da identidade
            services.AddIdentity<User, ShokuDexRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<FoodLogContext>();

            //Definição da base de dados para autenticação e autorização
            services.AddDbContext<FoodLogContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("Online"));
                });

            services.AddControllersWithViews();
            services.AddSwaggerGen(
                (x) =>
                {
                    x.SwaggerDoc("v1", new OpenApiInfo() { Title = "ShokuDex", Version = "v1" });
                });

            //CORS irresponsável
            services.AddCors(x => x.AddPolicy("Default", (builder) => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Clear();
                o.ViewLocationFormats.Add("/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/FoodInfoViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/UserInfoViews/{1}/{0}" + RazorViewEngine.ViewExtension);
                o.ViewLocationFormats.Add("/Views/OtherViews/{1}/{0}" + RazorViewEngine.ViewExtension);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<ShokuDexRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options => options.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.ApiDescription));


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();


            app.UseAuthorization();
            SetupRolesAndUsers(userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void SetupRolesAndUsers(UserManager<User> userManager, RoleManager<ShokuDexRole> roleManager)
        {
            if (roleManager.FindByNameAsync("Admin").Result == null) roleManager.CreateAsync(new ShokuDexRole() { Name = "Admin" }).Wait();
            if (roleManager.FindByNameAsync("User").Result == null) roleManager.CreateAsync(new ShokuDexRole() { Name = "User" }).Wait();
            if (roleManager.FindByNameAsync("Nutricionist").Result == null) roleManager.CreateAsync(new ShokuDexRole() { Name = "Nutricionist" }).Wait();
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var profile = new Profiles("Administrator", "", "Neutral", 100.00, DateTime.Now, "admin@shokudex.com", "", "", 0);
                var abo = new AccountBusinessObject(userManager, roleManager);
                var res = abo.Register("Admin", "Admin123!#", profile, "Admin").Result;
                var roleRes = userManager.AddToRoleAsync(userManager.FindByNameAsync("admin").Result, "Admin");
            }

        }
    }
}
