using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using StockNews.Models;
using StockNews.ViewModel;
using StockNews.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Win32;

namespace StockNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserDbContext db;

        public AccountController(UserDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {

            if (ModelState.IsValid)
            {


                var existingusr = await db.Users.FirstOrDefaultAsync(x=>x.Username == register.Username || x.Email==register.Email);

                if (existingusr != null)
                {

                    ModelState.AddModelError(String.Empty, "UserName or EmailId alredy Exists");
                    return View(register);

                }
                else
                {
                    var myUsr = new User
                    {
                        Username = register.Username,
                        Email = register.Email,
                        Password = PasswordHelper.EncodePassword(register.Password)
                    };

                    db.Users.Add(myUsr);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Login");
                }


            }

            return View(register);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {

                var logusr = await db.Users.SingleOrDefaultAsync(x => x.Email == login.EmailId || x.Password == login.Password);

                if (logusr != null)
                {
                    HttpContext.Session.SetString("Username",logusr.Username);
                    HttpContext.Session.SetString("UserId", logusr.UserId.ToString());
                    return RedirectToAction("Index","Home");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login User.");
                    return View(login);
                }
                       
            }
            return View(login);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login", "Account");
        }

    }
}
