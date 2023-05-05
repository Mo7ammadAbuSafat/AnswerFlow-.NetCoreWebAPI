using PersistenceLayer.Entities;

namespace BusinessLayer.Services.QuestionServices.Interfaces
{
    public interface IKeywordExtractorServices
    {
        Task<ICollection<Keyword>> GetKeywordsAsync(string text);
    }
}