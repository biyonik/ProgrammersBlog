using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProgrammersBlog.Entities.Concrete
{
    public class User: IdentityUser<int>, IEntity
    {
        public string Picture { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
