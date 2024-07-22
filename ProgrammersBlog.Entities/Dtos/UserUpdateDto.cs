using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(50, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        public string UserName { get; set; }

        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(100, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Telefon Numarası")]
        [Required(ErrorMessage = "{0} Boş Geçilemez!")]
        [MaxLength(13, ErrorMessage = "{0} {1} Karakterden Fazla Olamaz!")] // +90-555-555-55-55
        [MinLength(13, ErrorMessage = "{0} {1} Karakterden Az Olamaz!")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Fotoğraf Ekle")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        [DisplayName("Fotoğraf")]
        public string Picture { get; set; }
    }
}
