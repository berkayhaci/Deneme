using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Property(c => c.Text).IsRequired().HasMaxLength(1000);

            builder.HasOne<Article>(c => c.Article).WithMany(c => c.Comments).HasForeignKey(a => a.ArticleId); //bir makale çok yorum

            builder.Property(c => c.CreatedByName).IsRequired().HasMaxLength(50);

            builder.Property(c => c.ModifiedByName).IsRequired().HasMaxLength(50);

            builder.Property(c => c.CreatedDate).IsRequired();

            builder.Property(c => c.ModifiedDate).IsRequired();

            builder.Property(c => c.IsActive).IsRequired();

            builder.Property(c => c.IsDeleted).IsRequired();

            builder.Property(c => c.Note).HasMaxLength(500);

            builder.ToTable("Comments");

            //builder.HasData(new Comment
            //{
            //    Id = 1,
            //    ArticleId = 1,
            //    Text = "1500'lerden beri kullanılan standart Lorem Ipsum yığını, ilgilenenler için aşağıda yeniden verilmiştir. Cicero'nun de Finibus Bonorum et Malorumdan 1.10.32 ve 1.10.33 bölümleri de, H. Rackham'ın 1914 çevirisinin İngilizce versiyonlarıyla birlikte, tam orijinal halleriyle yeniden üretilmiştir.",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C# Makale Yorumu",
                
            //},
            //new Comment
            //{
            //    Id = 2,
            //    ArticleId = 2,
            //    Text = "1500'lerden beri kullanılan standart Lorem Ipsum yığını, ilgilenenler için aşağıda yeniden verilmiştir. Cicero'nun de Finibus Bonorum et Malorumdan 1.10.32 ve 1.10.33 bölümleri de, H. Rackham'ın 1914 çevirisinin İngilizce versiyonlarıyla birlikte, tam orijinal halleriyle yeniden üretilmiştir.",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "C++ Makale Yorumu",
            //},
            //new Comment
            //{
            //    Id = 3,
            //    ArticleId = 3,
            //    Text = "1500'lerden beri kullanılan standart Lorem Ipsum yığını, ilgilenenler için aşağıda yeniden verilmiştir. Cicero'nun de Finibus Bonorum et Malorumdan 1.10.32 ve 1.10.33 bölümleri de, H. Rackham'ın 1914 çevirisinin İngilizce versiyonlarıyla birlikte, tam orijinal halleriyle yeniden üretilmiştir.",
            //    IsActive = true,
            //    IsDeleted = false,
            //    CreatedByName = "InitialCreate",
            //    CreatedDate = DateTime.Now,
            //    ModifiedByName = "InitialCreate",
            //    ModifiedDate = DateTime.Now,
            //    Note = "JavaScript Makale Yorumu",
            //}
            //);



        }
    }
}
