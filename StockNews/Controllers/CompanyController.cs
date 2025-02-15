using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockNews.Models;

namespace StockNews.Controllers
{
    public class CompanyController : Controller
    {

        private readonly UserDbContext _context;


        public CompanyController(UserDbContext context)
        {
            _context = context;
        }




        public IActionResult Companylist()
        {
            return View();
        }


        public async Task<IActionResult> Search(string term)
        {
            var companies = await _context.Companies
                .Where(c => c.CompanyName.Contains(term))
                .Take(10) // Limit to 10 results
                .Select(c => new { c.CompanyId, c.CompanyName })
                .ToListAsync();
            return Json(companies);
        }


        //[HttpPost]
        //public async Task<IActionResult> Add(string companyName)
        //{
        //    var company = new Company { CompanyName = companyName };
        //    _context.Companies.Add(company);
        //    await _context.SaveChangesAsync();
        //    return Json(new { id = company.Id, companyName = company.CompanyName });
        //}



        [HttpPost]
        public async Task<IActionResult> Add(int id) 
        {
            
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("User is not logged in.");
            }

            int userId = int.Parse(userIdString);

            var existingUserCompany = await _context.UserCompanies
                .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CompanyId == id);

            if (existingUserCompany != null)
            {
                return BadRequest("User is already associated with this company.");
            }

            var userCompany = new UserCompany
            {
                UserId = userId,
                CompanyId = id
            };

            _context.UserCompanies.Add(userCompany);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }




        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            var userCompanies = await _context.UserCompanies
                .Where(uc => uc.UserId == userId)
                .Join(_context.Companies,
                      uc => uc.CompanyId,
                      c => c.CompanyId,
                      (uc, c) => new { c.CompanyId, c.CompanyName })
                .ToListAsync();

            return Json(userCompanies);
        }


    }
}
