using ProgrammersBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Concrete
{
    public class Article : EntityBase,IEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; } //Resim Yolu
        public DateTime Date { get; set; } // kullanıcı ya da adminin tuttuğu tarih
        public int ViewsCount { get; set; } = 0;//okunma sayısı
        public int CommentCount { get; set; } = 0;//yorum sayısı
        public string SeoAuthor { get; set; } //Seo optimizasyonu için seo tagleri
        public string SeoDescription { get; set; } // frontend tarafında arama motorlarına küçük açıklamalar
        public string SeoTags { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } //navigation property 
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; } //bir makale birden çok yorum, bir yorum bir makale



    }
}
