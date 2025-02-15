    namespace StockNews.Models
{
    public class NewsAndStockViewModel
    {
        public IEnumerable<NewsArticle> NewsArticles { get; set; }
        public CompanyInfo CompanyInfo { get; set; }
    }
}
