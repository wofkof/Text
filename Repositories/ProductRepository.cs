using Dapper;
using System.Data;

namespace ProductApi.Repositories
{
    public class ProductRepository
    {
        private readonly IDbConnection _db;

        public ProductRepository(IDbConnection db)
        {
            _db = db;
        }

        // 取得全部商品
        public async Task<IEnumerable<(int Id, string Name, decimal Price, string? Description, int CategoryId)>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products;";
            return await _db.QueryAsync<(int, string, decimal, string?, int)>(sql);
        }

        // 取得單一商品
        public async Task<(int Id, string Name, decimal Price, string? Description, int CategoryId)?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id;";
            return await _db.QueryFirstOrDefaultAsync<(int, string, decimal, string?, int)?>(sql, new { Id = id });
        }

        // 新增商品（SQLite 版本）
        public async Task<int> CreateAsync(string name, decimal price, string? description, int categoryId)
        {
            var sql = @"
                INSERT INTO Products (Name, Price, Description, CategoryId)
                VALUES (@Name, @Price, @Description, @CategoryId);
                SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<long>(sql, new { Name = name, Price = price, Description = description, CategoryId = categoryId });
            return (int)id;
        }

        // 更新商品
        public async Task<bool> UpdateAsync(int id, string name, decimal price, string? description, int categoryId)
        {
            var sql = @"
                UPDATE Products
                SET Name = @Name, Price = @Price, Description = @Description, CategoryId = @CategoryId
                WHERE Id = @Id;";

            var affected = await _db.ExecuteAsync(sql, new { Id = id, Name = name, Price = price, Description = description, CategoryId = categoryId });
            return affected > 0;
        }

        // 刪除商品
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id;";
            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        // 查詢特定價格範圍的商品（回傳 raw 資料）
        public async Task<IEnumerable<(int Id, string Name, decimal Price, string? Description, int CategoryId)>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var sql = "SELECT * FROM Products WHERE Price BETWEEN @MinPrice AND @MaxPrice";
            return await _db.QueryAsync<(int, string, decimal, string?, int)>(sql, new { MinPrice = minPrice, MaxPrice = maxPrice });
        }

        // 商品分類名稱查詢（回傳 raw 資料）
        public async Task<IEnumerable<(int Id, string Name, decimal Price, string? Description, int CategoryId, string CategoryName)>> GetWithCategoryAsync()
        {
            var sql = @"
                SELECT p.Id, p.Name, p.Price, p.Description, p.CategoryId, c.Name AS CategoryName
                FROM Products p
                JOIN Categories c ON p.CategoryId = c.Id";
            return await _db.QueryAsync<(int, string, decimal, string?, int, string)>(sql);
        }

        // 統計分類商品數（回傳 raw 資料）
        public async Task<IEnumerable<(int Id, string Name, int ProductCount)>> GetCategoryStatsAsync()
        {
            var sql = @"
                SELECT c.Id, c.Name, COUNT(p.Id) AS ProductCount
                FROM Categories c
                LEFT JOIN Products p ON c.Id = p.CategoryId
                GROUP BY c.Id, c.Name";
            return await _db.QueryAsync<(int, string, int)>(sql);
        }
    }
}
