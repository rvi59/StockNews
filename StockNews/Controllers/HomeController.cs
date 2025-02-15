
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockNews.Models;
using StockNews.Services;

namespace StockNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserDbContext _context;


        private readonly INewsService _newsService;
        private readonly IStockService _stockService;

       public HomeController(INewsService newsService, IStockService stockService,UserDbContext context)
        {
            _newsService = newsService;
            _stockService = stockService;
            _context = context;
        }


        //public async Task<IActionResult> Index()
        //{

        //    if (HttpContext.Session.GetString("Username") != null)
        //    {
        //        var news = await _newsService.GetNewsAsync();
        //        return View(news);
        //    }
        //    return RedirectToAction("Login", "Account");



        //}



        public async Task<IActionResult> Index()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var userCompanies = await _context.GetUserCompaniesAsync(userId);
            return View(userCompanies);
        }




        public async Task<IActionResult> GetNewsbyCompany(string CompanyName)
        {

            if (HttpContext.Session.GetString("Username") != null)
            {
                var news = await _newsService.GetNewsByCompanyAsync(CompanyName);

                string firstTwoWords = string.Join(" ", CompanyName.Split(' ').Take(2));

                var stockData = await _stockService.GetStockDataAsync(firstTwoWords);

                var StockNewsviewModel = new NewsAndStockViewModel
                {
                    NewsArticles = news,
                    CompanyInfo = stockData
                };

                return View(StockNewsviewModel);
            }
            return RedirectToAction("Login", "Account");
           
        }

       
    }
}
