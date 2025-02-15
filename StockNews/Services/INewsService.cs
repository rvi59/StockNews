using StockNews.Models;

namespace StockNews.Services
{
    public interface INewsService
    {
        Task<IEnumerable<NewsArticle>> GetNewsAsync();
        Task<IEnumerable<NewsArticle>> GetNewsByCompanyAsync(string CompanyName);
    }
}
