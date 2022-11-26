using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId); //DataResult içerisinde ister bir kategori istersem Liste taşırım
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleListDto>> GetAllByNoneDeleted();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId);
        Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName); //DTO:ViewModal olarak düşünülebilir.DTO içerisinde front tarafı ihtiyacı barındırır.
        Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> Delete(int articleId, string modifiedByName); //silme yapmıcak.IsDeleted değeri true yapıcak
        Task<IResult> HardDelete(int articleId);
    }
}
