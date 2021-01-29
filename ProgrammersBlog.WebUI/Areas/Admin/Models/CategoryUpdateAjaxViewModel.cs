using ProgrammersBlog.Entities.Dtos.Category;

namespace ProgrammersBlog.WebUI.Areas.Admin.Models
{
    public class CategoryUpdateAjaxViewModel
    {
        public CategoryUpdateDto CategoryUpdateDtoCategoryAddDto { get; set; }
        public string CategoryUpdatePartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}