using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T : class,IEntity,new() //Buraya sadece veri tabanı nesnelerinin geleceğini söylemiş olduk. New : veri tabanı nesnelerinin newleyebiliriz.
    {
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); //Tek kullanıcı ya da tek makale döner. var kullanici = repository.GetAsync(k=>k.Id==15) FİLTRELEME. Asenkron kullanımı =>Task.Örneğin 25idli makaleyi getirirken,makale ile birlikte kullanıcıyı ve yorumlarıda include etmek istiyoruz
         
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includeProperties); //null verme sebebi: makalelerin hepsinin de yükleyebilirz c# makaleleeride yükleyebiliriz.Eğer predicate değeri  null gelirse tüm hepsi yüklenicek,null gelmezse filtreye göre. 
        Task<T>AddAsync(T entity); //burada t tipinde dönmesini revize ettik. çünkü jquery tarafında dönen değeri json formata çevirdik
        Task<T>UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); //bu emailde kullanıcı hiç oldu mu ?
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);//kaç tane makale,kaç tane kullanıcı var


    }
}
