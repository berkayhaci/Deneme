﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.DTOs
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalı")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalı")]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not Alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalı")]
        public string Note { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi Mi?")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")]
        public bool IsDeleted { get; set; }
    }
}
