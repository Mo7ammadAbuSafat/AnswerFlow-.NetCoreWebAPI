using BusinessLayer.Models;
using BusinessLayer.Services.QuestionServices.Interfaces;
using Newtonsoft.Json;
using PersistenceLayer.Entities;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class KeywordExtractorServices : IKeywordExtractorServices
    {
        private readonly HttpClient httpClient;

        public KeywordExtractorServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ICollection<Keyword>> GetKeywordsAsync(string text)
        {
            string url = $"http://127.0.0.1:5000/keywords?text={text}";
            HttpResponseMessage response = await httpClient.GetAsync(url);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            KeywordsResponse apiResponse = JsonConvert.DeserializeObject<KeywordsResponse>(jsonResponse);

            ICollection<Keyword> keywords = apiResponse.keywords.Select(x => new Keyword() { name = x }).ToList();

            return keywords;
        }
    }
}
