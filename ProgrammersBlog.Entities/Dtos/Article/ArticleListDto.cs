using System.Collections.Generic;
using ProgrammersBlog.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos.Article
{
    public class ArticleListDto: DtoGetBase
    {
        public IList<Concrete.Article> Articles { get; set; }
        
    }
}