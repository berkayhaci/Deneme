using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable //Tüm repoları tek bir yerden yönetiyor olucaz
    {
        IArticleRepository Articles { get; } // unitofwork.articles. Sadece get işlemine erişim
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
       

        //_unitofwork.Categories.addasync();
        //_unitofwork.Categories.addasync(category); iki tane ekleme yaptıktan sonra veritabanına kaydetme yapıyoruz.biri hataya düşerse ex fırlatıcak
        //_unitofwork.Users.addasync(user);
        //_unitofwork.SaveAsync();
        Task<int> SaveAsync(); //Kayıt edilen sayısı

    }
}
