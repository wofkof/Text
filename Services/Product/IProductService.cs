using ProductApi.Dto;

namespace ProductApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(UpdateProductDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<ProductWithCategoryDto>> GetWithCategoryAsync();
    }
}
