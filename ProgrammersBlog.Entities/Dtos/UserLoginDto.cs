using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(5, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
