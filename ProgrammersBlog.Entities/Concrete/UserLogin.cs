using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Concrete
{
    public class UserLogin: IdentityUserLogin<int>, IEntity
    {
        
    }
}