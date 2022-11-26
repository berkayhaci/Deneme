using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;

namespace ProgrammersBlog.Mvc
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile)); //ekleme ya da update iþleminde profilleri bulamýyordu
            services.LoadMyServices();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Admin/User/Login");
                opt.LogoutPath = new PathString("/Admin/User/Logout");
                opt.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly=true,
                    SameSite=SameSiteMode.Strict, //en güvenlisidir
                    SecurePolicy=CookieSecurePolicy.SameAsRequest //always olmalý 
                };
                opt.SlidingExpiration=true;
                opt.ExpireTimeSpan = TimeSpan.FromDays(7);
                opt.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
