using SensitiveWordsAPI.Models;

namespace SensitiveWordsAPI.Services
{
    public interface ISensitiveWordsService
    {
        Task<string> SanitizeMessageAsync(string message);
        Task<IEnumerable<string>> GetAllWordsAsync();
        Task<int> AddWordAsync(string word);
        Task<int> UpdateWordAsync(int id, string word);
        Task<int> DeleteWordAsync(int id);
    }
}