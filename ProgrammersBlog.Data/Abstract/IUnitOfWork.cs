using System;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        // UnitOfWork.Articles
        IArticleRepository Articles { get; } 
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; } 
        // _unitOfWork.Users.AddAsync(user);
        // _unitOfWork.Categories.AddAsync(category)
        // _unitOfWork.SaveAsync()
        Task<int> SaveAsync();
    }
}