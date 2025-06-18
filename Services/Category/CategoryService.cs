using ProductApi.Dto;
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

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var raw = await _repo.GetAllAsync();
            return raw.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;

            return new CategoryDto
            {
                Id = c.Value.Id,
                Name = c.Value.Name
            };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var id = await _repo.CreateAsync(dto.Name);
            return new CategoryDto
            {
                Id = id,
                Name = dto.Name
            };
        }

        public async Task<bool> UpdateAsync(UpdateCategoryDto dto)
        {
            return await _repo.UpdateAsync(dto.Id, dto.Name);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}
