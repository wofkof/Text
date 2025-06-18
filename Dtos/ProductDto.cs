namespace ProductApi.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
    }
    public class CreateProductDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
    }
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public int CategoryId { get; set; }
    }

    public class ProductWithCategoryDto : ProductDto
    {
        public string CategoryName { get; set; } = "";
    }
}
