using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;

namespace ProgrammersBlog.Data.Concrete
{
    public class UserRepository : EfEntityRepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext contenxt) : base(contenxt)
        {
        }
    }
}
