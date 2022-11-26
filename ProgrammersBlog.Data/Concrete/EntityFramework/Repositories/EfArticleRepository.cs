using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Abstract;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfArticleRepository : EfEntityRepositoryBase<Article>, IArticleRepository //EfEntityeRepoda private readonly dbcontext var o yüzden burada ctor da base sınıftan türediğini söylemeliyiz.Eşitleme yapıyoruz
    {
        public EfArticleRepository(DbContext context) : base(context) //unit of work temeli yatıyor.Özel birşeyler kodalamak gerekirse aynı db contextleri kullanıcakalar.Unit of work yapısını kurarken dışardan kendi context yapımıızı vericez
        {
        }

        //public IList<Article> GetArticlesByCategory(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
