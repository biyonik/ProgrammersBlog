using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Dtos.Category;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.WebUI.Areas.Admin.Models;

namespace ProgrammersBlog.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET
        public async Task<ViewResult> Index()
        {
            var result = await _categoryService.GetAll();
            return View(result.Data);
        }

        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(categoryAddDto, "Ahmet Altun");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryAddAjaxViewModel = JsonSerializer.Serialize(
                        new CategoryAddAjaxViewModel
                        {
                            CategoryDto = result.Data,
                            CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                        }
                    );
                    return Json(categoryAddAjaxViewModel);
                }
            }
            var categoryAddAjaxErrorModel = JsonSerializer.Serialize(
                new CategoryAddAjaxViewModel
                {
                    CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                }
            );
            return Json(categoryAddAjaxErrorModel);
        }
    }
}