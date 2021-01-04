using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Concrete
{
    public class User: EntityBase, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
