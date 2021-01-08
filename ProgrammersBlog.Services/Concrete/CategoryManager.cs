using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos.Category;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            return category != null
                ? new DataResult<Category>(ResultStatus.Success, category)
                : new DataResult<Category>(ResultStatus.Error, "Böyle bir kategori bulunamadı!", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            IList<Category> categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            return categories.Count > -1
                ? new DataResult<IList<Category>>(ResultStatus.Success, categories)
                : new DataResult<IList<Category>>(ResultStatus.Error, "Hiçbir kategori bulunamadı!", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDeleted()
        {
            IList<Category> categories = await _unitOfWork.Categories.GetAllAsync(c => c.IsDeleted == false, c => c.Articles);
            return categories.Count > -1
                ? new DataResult<IList<Category>>(ResultStatus.Success, categories)
                : new DataResult<IList<Category>>(ResultStatus.Error, "Hiçbir kategori bulunamadı!", null);
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            Category category = new Category()
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Note,
                IsActive = categoryAddDto.IsActive,
                CreatedByName = createdByName,
                CreatedDate = DateTime.Now,
                ModifiedByName = createdByName,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };
            await _unitOfWork.Categories
                        .AddAsync(category)
                        .ContinueWith(t => _unitOfWork.SaveAsync());
            
            return new Result(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklendi!");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category != null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;

                await _unitOfWork.Categories
                            .UpdateAsync(category)
                            .ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellendi!");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı!");
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                await _unitOfWork.Categories
                    .DeleteAsync(category)
                    .ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori başarıyla silindi!");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı!");
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Success, $"{category.Name} adlı kategori veri tabanından başarıyla silindi!");
            }
            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı!");
        }
    }
}