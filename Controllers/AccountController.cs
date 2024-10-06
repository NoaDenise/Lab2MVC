using Microsoft.AspNetCore.Mvc;

namespace Lab2MVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin1234") // Enkel admin-inloggning
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ErrorMessage = "Felaktigt användarnamn eller lösenord.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
