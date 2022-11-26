using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email); // kullanıcıyı al
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index","Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Posta veya Şifre Hatalı");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Posta veya Şifre Hatalı");
                    return View();
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile); //picture tarafına resim adı eklendi
                var user = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password); //burada parola hashlenir
                if (result.Succeeded)
                {
                    return View(user);
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId); // farklı kullanım istersen findbyıd kullan
            var userUpdateDto = _mapper.Map<UserAddDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPicture = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPicture = true;
                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPicture)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    return View();

                }
                return View();

            }
            return View();
        }
        public async Task<string> ImageUpload(string userName, IFormFile pictureFile)
        {
            string wwwroot = _webHostEnvironment.WebRootPath; // dosya yolunu dinamik olarak verir.sonra resmin adı ve uzantısını almalıyız
            //berkayhacioglu    
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.Picture.FileName); //sonunda uzantı olmadan alıcak
            //.png ya da diğer formatları aldık
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime date = DateTime.Now;
            string fileName = $"{userName}_{date.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream); //ne yapacağını bilen fileStream var ordan alıcak
            }
            return fileName;
        }
        public bool ImageDelete(string pictureName) //resim adı 
        {
            string wwwroot = _webHostEnvironment.WebRootPath; //dosya yolu
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName); // resmin dosya yolu 
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
