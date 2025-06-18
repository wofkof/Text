using System.Data;
using Dapper;
using ProductApi.Models;

namespace ProductApi.Repositories
{
    public class CategoryRepository
    {
        private readonly IDbConnection _db;
        public CategoryRepository(IDbConnection db)
        {
            _db = db;
        }

        // 取得全部分類
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var sql = "SELECT * FROM Categories";
            return await _db.QueryAsync<Category>(sql);
        }

        // 取得單一分類
        public async Task<Category?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Categories WHERE Id = @Id";
            return await _db.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
        }

        // 新增分類
        public async Task<Category> CreateAsync(Category category)
        {
            var sql = @"
                INSERT INTO Categories (Name)
                VALUES (@Name);
                SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<long>(sql, category);
            category.Id = (int)id;
            return category;
        }

        // 更新分類
        public async Task<bool> UpdateAsync(Category category)
        {
            var sql = @"
                UPDATE Categories
                SET Name = @Name
                WHERE Id = @Id;";

            var affected = await _db.ExecuteAsync(sql, category);
            return affected > 0;
        }

        // 刪除分類
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Categories WHERE Id = @Id";

            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }
    }
}