using ProductApi.Dto;
using ProductApi.Repositories;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repo;

        public ProductService(ProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var raw = await _repo.GetAllAsync();
            return raw.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Value.Id,
                Name = p.Value.Name,
                Price = p.Value.Price,
                Description = p.Value.Description,
                CategoryId = p.Value.CategoryId
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var id = await _repo.CreateAsync(dto.Name, dto.Price, dto.Description, dto.CategoryId);
            return new ProductDto
            {
                Id = id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };
        }

        public async Task<bool> UpdateAsync(UpdateProductDto dto)
        {
            return await _repo.UpdateAsync(dto.Id, dto.Name, dto.Price, dto.Description, dto.CategoryId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var raw = await _repo.GetByPriceRangeAsync(minPrice, maxPrice);
            return raw.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId
            });
        }

        public async Task<IEnumerable<ProductWithCategoryDto>> GetWithCategoryAsync()
        {
            var raw = await _repo.GetWithCategoryAsync();
            return raw.Select(p => new ProductWithCategoryDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CategoryId = p.CategoryId,
                CategoryName = p.CategoryName
            });
        }

        public async Task<IEnumerable<CategoryStatsDto>> GetCategoryStatsAsync()
        {
            var raw = await _repo.GetCategoryStatsAsync();
            return raw.Select(r => new CategoryStatsDto
            {
                Id = r.Id,
                CategoryName = r.Name,
                ProductCount = r.ProductCount
            });
        }
    }
}
