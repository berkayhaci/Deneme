using AutoMapper;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.AutoMapper.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(dest=>dest.CreatedDate,opt=>opt.MapFrom(x=>DateTime.Now)); //1. kısım bizim kaynağımız neyi dönüştüreceğimiz 2.kısım nereye gideceğini neye dönüştüereceğini söyler... Burada Article içerisinde Created date var fakat addDto içerisinde yok.Dolayısıyla formember diyerek değer atamış olduk
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModifiedDate,opt=>opt.MapFrom(x=>DateTime.Now));

        }
    }
}
