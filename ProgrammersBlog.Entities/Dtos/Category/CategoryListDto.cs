using System.Collections.Generic;
using ProgrammersBlog.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos.Category
{
    public class CategoryListDto: DtoGetBase
    {
        public IList<Concrete.Category> Categories { get; set; }
    }
}