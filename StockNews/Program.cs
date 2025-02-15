using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StockNews.Models;
using Microsoft.AspNetCore.Session;
using StockNews.Services;

namespace StockNews
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<UserDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddSession(Options =>
            {
                Options.IdleTimeout = TimeSpan.FromMinutes(20);
                Options.Cookie.HttpOnly = true; 
                Options.Cookie.IsEssential = true; 

            });

            
            builder.Services.AddHttpClient<INewsService, NewsService>();
            builder.Services.AddHttpClient<IStockService, StockService>();



            var app = builder.Build();

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();
            
           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
