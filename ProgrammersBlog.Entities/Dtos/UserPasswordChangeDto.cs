using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Mevcut Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifre Tekrar")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni Şifre ile Yeni Şifre Tekrar eşleşmiyor!")]
        public string RepeatPassword { get; set; }

    }
}
