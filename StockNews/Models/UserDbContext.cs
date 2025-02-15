using Microsoft.EntityFrameworkCore;
using StockNews.ViewModel;

namespace StockNews.Models
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }




        public async Task<List<CompanyDTO>> GetUserCompaniesAsync(int userId)
        {
            // Fetch the companies using FromSqlRaw
            var companies = await Companies
                .FromSqlRaw("EXEC sp_GetUserCompanies @userId = {0}", userId)
                .ToListAsync(); // Materialize the query first

            // Now project to CompanyDto
            return companies.Select(c => new CompanyDTO
            {
                CompanyId = c.CompanyId,
                CompanyName = c.CompanyName
            }).ToList(); // Project to DTO
        }





    }
}
