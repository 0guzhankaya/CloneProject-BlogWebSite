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

namespace ProgrammersBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath; // string olarak wwwroot dosya yolunu dinamik olarak verir.
        }

        public async Task<IDataResult<ImageUploadedDto>> UploadUserImage(string userName, IFormFile pictureFile, string folderName = "userImages")
        {
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName); // .jpg

            DateTime dateTime = DateTime.Now;
            string newFileName = $"{userName} {dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";

            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            return new DataResult<ImageUploadedDto>(ResultStatus.SUCCESS, $"{userName} kullanıcısının resmi başarıyla yüklendi.",
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
