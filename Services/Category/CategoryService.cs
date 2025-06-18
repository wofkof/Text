using ProductApi.Dto;
using ProductApi.Models;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repo;
        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            var created = await _repo.CreateAsync(category);

            return new CategoryDto
            {
                Id = created.Id,
                Name = created.Name
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var category = await _repo.GetAllAsync();
            var result = category.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
            return result;
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
        {
            var category = new Category
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return await _repo.UpdateAsync(category);
        }
    }
}