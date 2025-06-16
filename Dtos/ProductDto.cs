namespace ProductApi.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public string Description { get; set; } = "";
    }
    public class CreateProductDto
    {
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public string Description { get; set; } = "";
    }
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Price { get; set; }
        public string Description { get; set; } = "";
    }
}
