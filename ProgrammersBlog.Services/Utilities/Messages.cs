using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Utilities
{
    public static class Messages
    {
        public static class Category
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir kategori bulunamadı!";
                return "Kategori bulunamadı!";
            }

            public static string Add(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla eklendi.";
            }

            public static string Update(string categoryName)
            {
                return $"{categoryName} başarıyla güncellendi.";
            }

            public static string Delete(string categoryName)
            {
                return $"{categoryName} adlı kategori başarıyla silinmiştir.";
            }

            public static string HardDelete(string categoryName)
            {
                return $"{categoryName} adlı kategori veritabanından başarıyla silinmiştir.";
            }
        }

        public static class Article
        {
            public static string NotFound(bool isPlural)
            {
                if (isPlural) return "Hiçbir makale bulunamadı!";
                return "Makale bulunamadı!";
            }

            public static string Add(string articleName)
            {
                return $"{articleName} adlı makale başarıyla eklendi.";
            }

            public static string Update(string articleName)
            {
                return $"{articleName} başarıyla güncellendi.";
            }

            public static string Delete(string articleName)
            {
                return $"{articleName} adlı kategori başarıyla silinmiştir.";
            }

            public static string HardDelete(string articleName)
            {
                return $"{articleName} adlı kategori veritabanından başarıyla silinmiştir.";
            }
        }
    }
}
