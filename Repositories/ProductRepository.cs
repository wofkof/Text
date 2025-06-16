using Dapper;
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
                INSERT INTO Products (Name, Price)
                VALUES (@Name, @Price);
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
                SET Name = @Name, Price = @Price
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
    }
}
