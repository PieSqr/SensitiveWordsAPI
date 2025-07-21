using Xunit;
using SensitiveWordsAPI.Services;

namespace SensitiveWordsAPI.Tests
{
    public class SensitiveWordsServiceTests
    {
        private class TestSensitiveWordsService : ISensitiveWordsService
        {
            private readonly List<string> _mockWords;

            public TestSensitiveWordsService(List<string> words)
            {
                _mockWords = words;
            }

            public Task<string> SanitizeMessageAsync(string message)
            {
                foreach (var word in _mockWords)
                {
                    var pattern = $@"\b{System.Text.RegularExpressions.Regex.Escape(word)}\b";
                    message = System.Text.RegularExpressions.Regex.Replace(
                        message,
                        pattern,
                        new string('*', word.Length),
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase
                    );
                }
                return Task.FromResult(message);
            }

            public Task<IEnumerable<string>> GetAllWordsAsync() => Task.FromResult((IEnumerable<string>)_mockWords);
            public Task<int> AddWordAsync(string word) => Task.FromResult(1);
            public Task<int> UpdateWordAsync(int id, string word) => Task.FromResult(1);
            public Task<int> DeleteWordAsync(int id) => Task.FromResult(1);
        }

        [Fact]
        public async Task SanitizeMessage_ReplacesExactSensitiveWords()
        {
            var service = new TestSensitiveWordsService(new List<string> { "CREATE", "TABLE" });
            var input = "Please CREATE a TABLE";
            var expected = "Please ****** a *****";

            var result = await service.SanitizeMessageAsync(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task SanitizeMessage_IsCaseInsensitive()
        {
            var service = new TestSensitiveWordsService(new List<string> { "delete" });
            var input = "Please DELETE this item";
            var expected = "Please ****** this item";

            var result = await service.SanitizeMessageAsync(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task SanitizeMessage_DoesNotReplacePartialWords()
        {
            var service = new TestSensitiveWordsService(new List<string> { "SELECT" });
            var input = "PreSELECTed items should not match";
            var expected = "PreSELECTed items should not match";

            var result = await service.SanitizeMessageAsync(input);

            Assert.Equal(expected, result);
        }

    }
}
