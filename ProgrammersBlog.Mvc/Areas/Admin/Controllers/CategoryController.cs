using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            //if (result.ResultStatus==ResultStatus.Success) //result  döndük çünkü içinde result status durumuna göre değer dönücek
            //{
            //    return View(result.Data); // başarılıysa result içindeki data dönüyoruz 
            //}

            //return View();
            return View(result.Data); // sadece bunu döndük çünkü içinde herşey var

        }
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
    }
}
