using ProgrammersBlog.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos.Article
{
    public class ArticleDto: DtoGetBase
    {
        public Concrete.Article Article { get; set; }
    }
}