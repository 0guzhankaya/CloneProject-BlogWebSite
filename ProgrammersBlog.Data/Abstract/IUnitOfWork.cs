using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; } 
        ICategoryRepository Categories { get; } // _unitOfWork.Categories.AddAsync
        ICommentRepository Comments { get; }  // _unitOfWork.Categories.AddAsync(category);
        Task<int> SaveAsync(); // _unitOfWork.SaveAsync();
    }
}
