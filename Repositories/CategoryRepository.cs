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

        // 取得全部分類 (Raw)
        public async Task<IEnumerable<(int Id, string Name)>> GetAllAsync()
        {
            var sql = "SELECT Id, Name FROM Categories;";
            return await _db.QueryAsync<(int, string)>(sql);
        }

        // 取得單一分類 (Raw)
        public async Task<(int Id, string Name)?> GetByIdAsync(int id)
        {
            var sql = "SELECT Id, Name FROM Categories WHERE Id = @Id;";
            return await _db.QueryFirstOrDefaultAsync<(int, string)?>(sql, new { Id = id });
        }

        // 新增分類，回傳 Id
        public async Task<int> CreateAsync(string name)
        {
            var sql = @"
                INSERT INTO Categories (Name)
                VALUES (@Name);
                SELECT last_insert_rowid();";

            var id = await _db.ExecuteScalarAsync<long>(sql, new { Name = name });
            return (int)id;
        }

        // 更新分類
        public async Task<bool> UpdateAsync(int id, string name)
        {
            var sql = @"
                UPDATE Categories
                SET Name = @Name
                WHERE Id = @Id;";

            var affected = await _db.ExecuteAsync(sql, new { Id = id, Name = name });
            return affected > 0;
        }

        // 刪除分類
        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Categories WHERE Id = @Id;";

            var affected = await _db.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        internal async Task CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
