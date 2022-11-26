using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult     //IDataResult ihtiyacı: Veri taşımak için ve kategorileri listelediğimizde mesaj vermek için. "out T " ise hem Ilist,IENumarable hemde category alabilmesi için
    {
        public T Data { get;  } // Data içerisine ister kategori istersekte kategori listesi atabiliyor olucaz. new DataResult<Category>(ResultStatus.Success,category);

    }
}
