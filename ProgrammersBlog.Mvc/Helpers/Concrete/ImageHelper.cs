using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System.IO;
using System;
using System.Threading.Tasks;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Entities.ComplexTypes;

namespace ProgrammersBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "img";
        private const string userImagesFolder = "userImages";
        private const string postImagesFolder = "postImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath; // string olarak wwwroot dosya yolunu dinamik olarak verir.
        }

        public async Task<IDataResult<ImageUploadedDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null)
        {
            /* Eğer folderName değişkeni null gelir ise, o zaman resim tipine göre (PictureType) klasör ataması yapılır. */
            folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

            /* Eğer folderName değişkeni ile gelen klasör adı sistemimizde mevcut değilse, yeni bir klasör oluşturulur.*/
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            /* Resmin yüklenme sırasındaki ilk adı oldFileName adlı değişkene atanır. */
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);

            /* Resmin uzantısı fileExtension adlı değişkene aktarılır.*/
            string fileExtension = Path.GetExtension(pictureFile.FileName); // .jpg

            DateTime dateTime = DateTime.Now;

            /*
             * Parametre ile gelen değerler kullanılarak yeni bir resim adı oluşturulur.
             * Örn : OguzhanKaya_587_5_38_12_3_10_2024.png
             */

            string newFileName = $"{name} {dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            /* Kendi parametrelerimiz ile sistemimize uygun yeni bir dosya yolu (path) oluşturulur. */
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            /* Sistemimiz için oluşturulan yeni dosya yoluna resim kopyalanır. */
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            /* Resim tipine göre kullanıcı için bir mesaj oluşturulur. */
            string message = pictureType == PictureType.User ? $"{name} kullanıcısının resmi başarıyla yüklendi." : $"{name} makalenin resmi başarıyla yüklendi.";

            return new DataResult<ImageUploadedDto>(ResultStatus.SUCCESS, message,
                new ImageUploadedDto
                {
                    FullName = $"{folderName}/{newFileName}",
                    OldName = oldFileName,
                    Extension = fileExtension,
                    FolderName = folderName,
                    Path = path,
                    Size = pictureFile.Length
                });
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
            var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);

            // fileToDelete'de dosya var ise sil.
            if (System.IO.File.Exists(fileToDelete))
            {
                var fileInfo = new FileInfo(fileToDelete); // returns file info.
                var imageDeletedDto = new ImageDeletedDto
                {
                    FullName = pictureName,
                    Extension = fileInfo.Extension,
                    Path = fileInfo.FullName,
                    Size = fileInfo.Length
                };

                System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.SUCCESS, imageDeletedDto);
            }
            else
            {
                return new DataResult<ImageDeletedDto>(ResultStatus.ERROR, $"Böyle bir resim bulunamadı.", null);
            }
        }
    }
}
