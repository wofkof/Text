using Microsoft.AspNetCore.Mvc;
using ProductApi.Dto;
using ProductApi.Models;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(CreateProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            if (id != dto.Id) return BadRequest("ID 不一致");

            var success = await _service.UpdateAsync(dto);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            if (minPrice > maxPrice) return BadRequest("最小價格不可大於最大價格");
            var result = await _service.GetByPriceRangeAsync(minPrice, maxPrice);
            return Ok(result);
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<ProductWithCategoryDto>>> GetWithCategory()
        {
            var result = await _service.GetWithCategoryAsync();
            return Ok(result);
        }

        [HttpGet("stats")]
        public async Task<ActionResult<IEnumerable<CategoryStatsDto>>> GetCategoryStats()
        { 
            var result = await _service.GetCategoryStatsAsync();
            return Ok(result);
        }
    }
}
