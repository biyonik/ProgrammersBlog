using ProgrammersBlog.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos.Category
{
    public class CategoryDto: DtoGetBase
    {
        public Concrete.Category Category { get; set; }
    }
}