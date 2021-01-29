using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId, c => c.Articles);
            return category != null
                ? new DataResult<CategoryDto>(ResultStatus.Success, new CategoryDto()
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                })
                : new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı!", new CategoryDto
                {
                    Category = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Böyle bir kategori bulunamadı!"
                });
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }

            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı!", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            IList<Category> categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            return categories.Count > -1
                ? new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
                {
                    ResultStatus = ResultStatus.Success,
                    Categories = categories
                })
                : new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı!", new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Hiçbir kategori bulunamadı!"
                });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            IList<Category> categories =
                await _unitOfWork.Categories.GetAllAsync(c => c.IsDeleted == false, c => c.Articles);
            return categories.Count > -1
                ? new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
                {
                    ResultStatus = ResultStatus.Success,
                    Categories = categories
                })
                : new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı!", new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Hiçbir kategori bulunamadı!"
                });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            IList<Category> categories =
                await _unitOfWork.Categories.GetAllAsync(c => c.IsDeleted == false && c.IsActive, c => c.Articles);
            return categories.Count > -1
                ? new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto()
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                })
                : new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı!", new CategoryListDto
                {
                    Categories = null,
                    ResultStatus = ResultStatus.Error,
                    Message = "Hiçbir kategori bulunamadı!"
                });
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            Category category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories
                .AddAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarıyla eklendi!", new CategoryDto
            {
                Category = addedCategory,
                Message = $"{addedCategory.Name} adlı kategori başarıyla eklendi!",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            Category category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            category.ModifiedDate = DateTime.Now;
            
            var updatedCategory = await _unitOfWork.Categories
                .UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellendi!", new CategoryDto
            {
                Category = updatedCategory,
                Message = $"{updatedCategory.Name} adlı kategori başarıyla güncellendi!",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories
                    .UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} adlı kategori başarıyla güncellendi!", new CategoryDto
                {
                    Category = deletedCategory,
                    Message = $"{deletedCategory.Name} adlı kategori başarıyla silindi!",
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı!", new CategoryDto()
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir kategori bulunamadı!"
            });
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success,
                    $"{category.Name} adlı kategori veri tabanından başarıyla silindi!");
            }

            return new Result(ResultStatus.Error, "Böyle bir kategori bulunamadı!");
        }
    }
}