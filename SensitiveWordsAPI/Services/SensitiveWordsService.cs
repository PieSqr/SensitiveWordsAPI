using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;
using SensitiveWordsAPI.Models;

namespace SensitiveWordsAPI.Services
{
    public class SensitiveWordsService : ISensitiveWordsService
    {
        private readonly IDbConnection _db;

        public SensitiveWordsService(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnection"));
        }

        public async Task<string> SanitizeMessageAsync(string message)
        {
            var words = await GetAllWordsAsync();
            foreach (var word in words)
            {
                var pattern = $@"\b{Regex.Escape(word)}\b";
                message = Regex.Replace(message, pattern, new string('*', word.Length), RegexOptions.IgnoreCase);
            }
            return message;
        }

        public async Task<IEnumerable<string>> GetAllWordsAsync()
        {
            return await _db.QueryAsync<string>("SELECT Word FROM SensitiveWords");
        }

        public async Task<int> AddWordAsync(string word)
        {
            return await _db.ExecuteAsync("INSERT INTO SensitiveWords (Word) VALUES (@word)", new { word });
        }

        public async Task<int> UpdateWordAsync(int id, string word)
        {
            return await _db.ExecuteAsync("UPDATE SensitiveWords SET Word = @word WHERE Id = @id", new { id, word });
        }

        public async Task<int> DeleteWordAsync(int id)
        {
            return await _db.ExecuteAsync("DELETE FROM SensitiveWords WHERE Id = @id", new { id });
        }
    }
}