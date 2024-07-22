using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class ArticleManager : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ArticleAddDto articleAddDto, string createdByName)
        {
            var article = _mapper.Map<Article>(articleAddDto);
            article.UserId = 1;
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();

            return new Result(ResultStatus.SUCCESS, Messages.Article.Add(article.Title));
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsDeleted = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await _unitOfWork.Articles.UpdateAsync(article).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.SUCCESS, Messages.Article.Delete(article.Title));
            }

            return new Result(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId, a => a.User, a => a.Category);

            if (article != null)
            {
                return new DataResult<ArticleDto>(ResultStatus.SUCCESS, new ArticleDto
                {
                    Article = article,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<ArticleDto>(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAll()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.SUCCESS, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<ArticleListDto>(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategory(int categoryId)
        {
            // Önce kategorinin var olup olmadığını kontrol edildi.
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);

            if (result)
            {
                var articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);

                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStatus.SUCCESS, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.SUCCESS
                    });
                }

                return new DataResult<ArticleListDto>(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: true), null);
            }

            return new DataResult<ArticleListDto>(ResultStatus.ERROR, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeleted()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, ar => ar.User, ar => ar.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.SUCCESS, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<ArticleListDto>(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, ar => ar.User, ar => ar.Category);

            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.SUCCESS, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.SUCCESS
                });
            }

            return new DataResult<ArticleListDto>(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);

            if (result)
            {
                var article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await _unitOfWork.Articles.DeleteAsync(article);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.SUCCESS, Messages.Article.HardDelete(article.Title));
            }

            return new Result(ResultStatus.ERROR, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var article = _mapper.Map<Article>(articleUpdateDto);
            article.ModifiedByName = modifiedByName;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.SUCCESS, Messages.Article.Update(article.Title));
        }

        public async Task<IDataResult<int>> Count()
        {
            var articleCount = await _unitOfWork.Articles.CountAsync();

            if (articleCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, articleCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmeyen bir hata ile karşılaşıldı!", -1);
            }
        }

        public async Task<IDataResult<int>> CountByIsDeleted()
        {
            var articleCount = await _unitOfWork.Articles.CountAsync(a => !a.IsDeleted); // Silinmeyen Makaleleri getirecek predicate verildi.

            if (articleCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, articleCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmeyen bir hata ile karşılaşıldı!", -1);
            }
        }
    }
}
