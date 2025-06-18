namespace ProductApi.Dto
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class CreateCategoryDto
    {
        public string Name { get; set; } = "";
    }

    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }

    public class CategoryStatsDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = "";
        public int ProductCount { get; set; }
    }
}