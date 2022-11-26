using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.DTOs
{
    public class ArticleDto : DtoGetBase //burada result status taşıyoruz çünkü view içerisinde resultstatuse göre işlem yapıcaz
    {
        public Article Article { get; set; }
        

    }
}
