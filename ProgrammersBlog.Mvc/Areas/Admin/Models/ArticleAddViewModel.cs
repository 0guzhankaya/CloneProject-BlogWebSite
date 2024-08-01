using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Concrete;
using System.Collections.Generic;

namespace ProgrammersBlog.Mvc.Areas.Admin.Models
{
    public class ArticleAddViewModel
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

        [DisplayName("Küçük Resim")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        public IFormFile ThumbnailFile { get; set; }

        [DisplayName("Tarih")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Yazar Adı")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(50, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoAuthor { get; set; }

        [DisplayName("Makale Açıklaması")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(150, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoDescription { get; set; }

        [DisplayName("Makale Etiketleri")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        [MaxLength(70, ErrorMessage = "{0} Alanı {1} Karakterden Büyük Olmamalıdır.")]
        [MinLength(0, ErrorMessage = "{0} Alanı {1} Karakterden Küçük Olmamalıdır.")]
        public string SeoTags { get; set; }

        [DisplayName("Kategori")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        public int CategoryId { get; set; }
        public IList<Category> Categories { get; set; } // SelectList doldurmak için kullanılacak.

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} Alanı Boş Bırakılamaz.")]
        public bool IsActive { get; set; }
    }
}
