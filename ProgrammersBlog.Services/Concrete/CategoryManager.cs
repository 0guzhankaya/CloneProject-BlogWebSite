﻿using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using AutoMapper;
using ProgrammersBlog.Services.Utilities;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">categoryAddDto tipinde eklenecek kategori bilgileri.</param>
        /// <param name="createdByName">string tipinde kullanıcı adı, maksimum 50 karakter.</param>
        /// <returns>Asenkron bir operasyon ile Task olarak ekleme işlemin sonucunu DataResult tipinde döner.</returns>
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.SUCCESS, Messages.Category.Add(addedCategory.Name), new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.SUCCESS,
                Message = Messages.Category.Add(addedCategory.Name)
            });
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.SUCCESS, Messages.Category.Delete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.SUCCESS,
                    Message = Messages.Category.Delete(deletedCategory.Name)
                });
            }

            return new DataResult<CategoryDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.ERROR,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);

            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.SUCCESS, new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<CategoryDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural:false), new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.ERROR,
                Message = Messages.Category.NotFound(isPlural:false)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.SUCCESS, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.ERROR,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.SUCCESS, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.ERROR,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsDeleted && !c.IsActive, c => c.Articles);

            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.SUCCESS, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: true), null);
        }

        /// <summary>
        /// Verilen Id parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer Id değeri.</param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucu DataResult tipinde geriye döner.</returns>
        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);

            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.SUCCESS, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: false), null);
            }
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.SUCCESS, Messages.Category.HardDelete(category.Name));
            }

            return new Result(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: false));
        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            var category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.SUCCESS, Messages.Category.Update(updatedCategory.Name), new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.SUCCESS,
                Message = Messages.Category.Update(updatedCategory.Name)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync();

            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmedik bir hata ile karşılaşıldı!", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await _unitOfWork.Categories.CountAsync(c => !c.IsDeleted); // Silinmeyen Kategorileri getirecek predicate verildi.

            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmedik bir hata ile karşılaşıldı!", -1);
            }
        }
    }
}
