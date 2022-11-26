﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalı")]
        public string UserName { get; set; }

        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalı")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [DisplayName("Telefon No")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(13, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(13, ErrorMessage = "{0} {1} karakterden küçük olmamalı")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Resim Ekle")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        [DisplayName("Resim")]
        public string Picture { get; set; }
    }
}
