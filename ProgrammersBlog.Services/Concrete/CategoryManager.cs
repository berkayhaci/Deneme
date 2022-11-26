using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory= await _unitOfWork.Categories.AddAsync(category);

            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir",new CategoryDto
            {
                Category =addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir"
            });

            /* await _unitOfWork.Categories.AddAsync(new Category
             {
                 Name = categoryAddDto.Name,
                 Description = categoryAddDto.Description,
                 Note = categoryAddDto.Note,
                 IsActive = categoryAddDto.IsActive,
                 CreatedByName = createdByName,
                 CreatedDate = DateTime.Now,
                 ModifiedByName = createdByName,
                 ModifiedDate = DateTime.Now,
                 IsDeleted = false

             }).ContinueWith(t => _unitOfWork.SaveAsync());
             //await _unitOfWork.SaveAsync(); bunun yerine üsttekini ekledik
             return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir");*/

        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} başarıyla silinmiştir");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(x => x.Id == categoryId, x => x.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message= "Böyle bir kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, x => x.Articles);
            if (categories.Count > -1) //belki hiç kategori eklemedik 0 tane de olabilir ;) Admin panelinden yapmak istiyor olabiliriz
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori Bulunamadı", new CategoryListDto
            {
                Categories= null,
                ResultStatus= ResultStatus.Error,
                Message = "Kategori Bulunamadı."
            }); //Buradaki işlemi sadece view tarafında rahat etmek için yaptık.Eğer olumluysa categorileri listeiyoruz ya burada eğer error durumsa da boş kategori göstermnek yerine hata mesajını direk viewda vericez controller tarafında uğraşmadan
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNoneDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted, x => x.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
                //return new DataResult<CategoryListDto>(ResultStatus.Success,_mapper.Map<CategoryListDto>(categories));


            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori Bulunamadı", null);

        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNoneDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(x => !x.IsDeleted && x.IsActive, x => x.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Kategori Bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Success, $"{category.Name} başarıyla veri tabanından silinmiştir");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updateCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir",new CategoryDto
            {
                Category=updateCategory,
                ResultStatus=ResultStatus.Success,
                Message = $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir"

            });

            /*var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} başarıyla güncellenmiştir");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı");*/
        }
    }
}
