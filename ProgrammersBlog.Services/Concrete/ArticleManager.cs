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
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = 1;

            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"{articleAddDto.Title} başlıklı makale başarıyla eklenmiştir.");


        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);
                article.IsDeleted=true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate=DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,$"{article.Title} başarıyla silinmiştir");

            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.Category, a => a.User);
            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error, "Böyle bir makale bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, x => x.User, x => x.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(x => x.Id == categoryId);
            if (result)
            {
                var articleCategory = await _unitOfWork.Articles.GetAllAsync(x => x.CategoryId == categoryId && !x.IsDeleted && x.IsActive, x => x.User, x => x.Category);
                if (articleCategory.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                    {
                        Articles = articleCategory,
                        ResultStatus = ResultStatus.Success
                    });
                }
                return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);


        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted && x.IsActive, xy => xy.User, xy => xy.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNoneDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(x => !x.IsDeleted, x => x.Category, x => x.User);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(x => x.Id == articleId);


                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{article.Title} başarıyla veritabanından silinmiştir");

            }
            return new Result(ResultStatus.Error, "Böyle bir makale bulunamadı");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(x => x.Id == articleUpdateDto.Id);
            if (result)
            {
                var article = _mapper.Map<Article>(articleUpdateDto);
                article.ModifiedByName = modifiedByName;
                await _unitOfWork.Articles.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{articleUpdateDto.Title} başlıklı makale başarıyla güncellenmiştir");
            }
            return new Result(ResultStatus.Error, $"{articleUpdateDto.Title} başlıklı makale bulunamamıştır");
            
        }
    }
}
