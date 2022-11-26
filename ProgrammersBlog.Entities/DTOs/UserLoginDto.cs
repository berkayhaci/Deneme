using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.DTOs
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalı")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilemez")] // 0 değeri display name alanını buraya taşır
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamaz  ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalı")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
