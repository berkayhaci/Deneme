using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProgrammersBlogContext _context;
        private EfArticleRepository _articleRepository; //readonly yapmıyoruz çünkü new leme işleme yapıcaz
        private EfCategoryRepository _categoryRepository;
        private EfCommentRepository _commentRepository;
    


        public UnitOfWork(ProgrammersBlogContext context)
        {
            _context = context;
        }

        /*
         * Dispose Metodu içerisinde contextimizi dispose ediyor olucaz.
         * Aşağıdaki interface repolar çağırıldığında,somut yani concrete yapısını return edicez
         * Son olarak save async metodu var oradada saveChange() metodunu çağırıcaz
         * Db context değil de kendi contextimizi yapıyor olucaz çünkü repolar ctro içinde dbcontext nesnesi istiyor.
         * Reoların somutlarına niye ihtiyaç duyuyoruz dersek, interfaceleri new leyemeyiz. Return edemeyiz normal bir interface`i.
         */

        /*
         * Aşağıdaki IArticleRepository Articles =>  _articleRepository "Return olarak _articleRepo veriyoruz yani somut örneğini. Fakat burada somut hali herhangi birşeye atanmış değil new lemedik. O yüzden null ise yani new lenmemiş ise ?? operarötürü alternatif değer döndürür"
         */
        public IArticleRepository Articles => _articleRepository ?? new EfArticleRepository(_context);  //get metodu

        public ICategoryRepository Categories => _categoryRepository ?? new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ?? new EfCommentRepository(_context);

       

        public async ValueTask DisposeAsync() //Garbag Colector yani bellek yapısnın yönetimi. C# bellek yönetimi belirli aralıklarla çalışır
        {
            await _context.DisposeAsync(); //contextte asenkron olarak kontrol ediyor olucaz.diğer işlemleri beklemeye gerek kalmıyor
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync(); //save changes değeri bize int değer döner o yüzden hata vermedi.
        }
    }
}
