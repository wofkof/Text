using Dapper;
using ProductApi.Dto;
using ProductApi.Models;
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
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products;";
            return await _db.QueryAsync<Product>(sql);
        }

        // 取得單一商品
        public async Task<Product?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id;";
            return await _db.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        // 新增商品（SQLite 版本）
        public async Task<Product> CreateAsync(Product product)
        {
            var sql = @"
                INSERT INTO Products (Name, Price, Description, CategoryId)
                VALUES (@Name, @Price, @Description, @CategoryId);
                SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<long>(sql, product);
            product.Id = (int)id;
            return product;
        }

        // 更新商品
        public async Task<bool> UpdateAsync(Product product)
        {
            var sql = @"
                UPDATE Products
                SET Name = @Name, Price = @Price, Description = @Description, CategoryId = @CategoryId
                WHERE Id = @Id;";

            var affected = await _db.ExecuteAsync(sql, product);
            return affected > 0;
        }

        // 刪除商品
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Products WHERE Id = @Id;";
            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        // 查詢特定價格範圍的商品
        public async Task<IEnumerable<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var sql = "SELECT * FROM Products WHERE Price BETWEEN @MinPrice AND @MaxPrice";
            return await _db.QueryAsync<Product>(sql, new { MinPrice = minPrice, MaxPrice = maxPrice });
        }

        // 顯示分類名稱
        public async Task<IEnumerable<ProductWithCategoryDto>> GetWithCategoryAsync()
        {
            var sql = @"
                SELECT p.Id, p.Name, p.Price, p.Description, p.CategoryId, c.Name AS CategoryName
                FROM Products p
                JOIN Categories c ON p.CategoryId = c.Id";
            var result = await _db.QueryAsync<ProductWithCategoryDto>(sql);
            return result;
        }
    }
}
