using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions // extensions yapısı için static gerekli 
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection services)
        {
            services.AddDbContext<ProgrammersBlogContext>();

            services.AddIdentity<User, Role>(opt =>
            {
                //user password options
                opt.Password.RequireDigit=false;
                opt.Password.RequiredLength=5;
                opt.Password.RequiredUniqueChars=0; //kaç farklı özel karakter
                opt.Password.RequireNonAlphanumeric=false; //örnek olarak @ ! $ gibi işaretlerin kullanılmasını sağlar.Üsttekinden farkı burası zorunlu mu değil mi diye
                opt.Password.RequireLowercase=false;  //küçük harf zorunluluğu
                opt.Password.RequireUppercase=false; //büyük harf zorunluluğu
                //username and email
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";
                opt.User.RequireUniqueEmail=true;
            }).AddEntityFrameworkStores<ProgrammersBlogContext>();
            
            
            services.AddScoped<IUnitOfWork, UnitOfWork>(); //DbContext özünde scopedtur. Scoped => Request esnasında yeni bir instance alınır ve o request sonlanana kadar aynı nesne kullanılır.
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IArticleService, ArticleManager>();

            return services;
        }
    }
}
