﻿using System.Text.Json;
using System.Text.Json.Serialization;
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
            var result = await _categoryService.GetAllByNonDeleted();
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
                            CategoryAddPartial =
                                await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
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

        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_CategoryUpdatePartial", result.Data);
            }

            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, "Ahmet Altun");
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var categoryUpdateAjaxViewModel = JsonSerializer.Serialize(
                        new CategoryUpdateAjaxViewModel
                        {
                            CategoryDto = result.Data,
                            CategoryUpdatePartial =
                                await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
                        }
                    );
                    return Json(categoryUpdateAjaxViewModel);
                }
            }

            var categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(
                new CategoryUpdateAjaxViewModel
                {
                    CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
                }
            );
            return Json(categoryUpdateAjaxErrorModel);
        }

        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
            return Json(categories);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, "Ahmet Altun");
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
    }
}