using StockNews.Models;

namespace StockNews.Services
{
    public interface IStockService
    {
        Task<CompanyInfo> GetStockDataAsync(string companyName);
    }
}
