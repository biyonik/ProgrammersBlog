using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Concrete
{
    public class Comment: EntityBase, IEntity
    {
        public string Text { get; set; }
        public int ArticeId { get; set; }
        public virtual Article Article { get; set; }
    }
}
