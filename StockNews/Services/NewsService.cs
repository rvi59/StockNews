using Newtonsoft.Json;
using StockNews.Models;


namespace StockNews.Services
{
    public class NewsService : INewsService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public NewsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["NewsDataApi:ApiKey"];
        }


        public async Task<IEnumerable<NewsArticle>> GetNewsAsync()
        {
            var response = await _httpClient.GetAsync("https://newsdata.io/api/1/latest?apikey="+_apiKey+"&country=in&language=en");
            response.EnsureSuccessStatusCode();

            // Read and parse the response
            var content = await response.Content.ReadAsStringAsync();
            var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(content);

            return newsResponse.Results;
        }




        public async Task<IEnumerable<NewsArticle>> GetNewsByCompanyAsync(string CompanyName)
        {
            
            var response = await _httpClient.GetAsync("https://newsdata.io/api/1/news?apikey=" + _apiKey + "&q="+CompanyName+"&country=in&language=en");
            response.EnsureSuccessStatusCode();

          
            var content = await response.Content.ReadAsStringAsync();
            var newsResponse = JsonConvert.DeserializeObject<NewsResponse>(content);

            return newsResponse.Results;
        }



    }
}
