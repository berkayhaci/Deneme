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
    public interface ICategoryService 
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId); //DataResult içerisinde ister bir kategori istersem Liste taşırım
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNoneDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNoneDeletedAndActive(); //silinmeyen ve aktif olan

        Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto,string createdByName); //DTO:ViewModal olarak düşünülebilir.DTO içerisinde front tarafı ihtiyacı barındırır.
        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto,string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName); //silme yapmıcak.IsDeleted değeri true yapıcak
        Task<IResult> HardDelete(int categoryId);
    }
}
