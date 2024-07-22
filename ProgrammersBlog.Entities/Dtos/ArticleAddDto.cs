using ProgrammersBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class ArticleAddDto
    {
        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(100, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string Title { get; set; }

        [DisplayName("İçerik")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MinLength(20, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string Content { get; set; }

        [DisplayName("Thumbnail")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(250, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string Thumbnail { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Bilgisi")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(50, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(150, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoDescription { get; set; }

        [DisplayName("Etiketler")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(70, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        public bool IsActive { get; set; }
    }
}
