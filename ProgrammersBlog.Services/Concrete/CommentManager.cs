using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Services.Abstract;
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
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommentManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync();

            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmeyen bir hata ile karşılaşıldı!", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted); // Silinmeyen Comments getirecek predicate verildi.

            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.SUCCESS, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.ERROR, $"Beklenmeyen bir hata ile karşılaşıldı!", -1);
            }
        }
    }
}
