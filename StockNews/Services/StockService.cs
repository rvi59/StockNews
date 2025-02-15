using Newtonsoft.Json;
using StockNews.Models;

namespace StockNews.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;


        public StockService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["StockApi:ApiKey"];
        }



        public async Task<CompanyInfo> GetStockDataAsync(string companyName)
        {
            _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);

            var response = await _httpClient.GetAsync("https://stock.indianapi.in/stock?name="+companyName);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var companyInfo = JsonConvert.DeserializeObject<CompanyInfo>(content);

            return companyInfo;
        }
    }
}
