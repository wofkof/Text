using ProductApi.Models;
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
            var products = await _repo.GetAllAsync();
            return products.Select(p => new ProductDto
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
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            var created = await _repo.CreateAsync(product);

            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price,
                Description = created.Description,
                CategoryId = created.CategoryId
            };
        }

        public async Task<bool> UpdateAsync(UpdateProductDto dto)
        {
            var product = new Product
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            return await _repo.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await _repo.GetByPriceRangeAsync(minPrice, maxPrice);
            return products.Select(p => new ProductDto
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
            return await _repo.GetWithCategoryAsync();
        }
    }
}
