using Microsoft.AspNetCore.Mvc;
using MusilZavěrečnýProjekt.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MusilZavěrečnýProjekt.Controllers
{
    public class RezervaceController : Controller
    {
        public List<User> Users { get; set; }
        public RezervaceDBContext DbContext { get; set; }

        public RezervaceController()
        {
            DbContext = new();
            Users = DbContext.users.ToList();
        }
        public IActionResult Index()
        {
            // Simple check: if not logged in (no TempData flag), redirect to Login
            if (TempData["LoggedInUser"] == null)
            {
                return RedirectToAction("Login");
            }
            TempData.Keep("LoggedInUser");
            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existing = DbContext.users.FirstOrDefault(u => EF.Property<string>(u, "Email") == user.Email);

            if (existing == null)
            {
                ModelState.AddModelError(string.Empty, "Uživatel neexistuje. Zaregistrujte se.");
                return View(user);
            }

            // Plain text password comparison
            if (existing.Password == user.Password)
            {
                TempData["LoggedInUser"] = existing.Username;
                TempData["Message"] = $"Přihlášen: {existing.Username}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Nesprávné heslo.");
            return View(user);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            var existing = DbContext.users.FirstOrDefault(u => EF.Property<string>(u, "Email") == user.Email || EF.Property<string>(u, "Username") == user.Username);
            if (existing != null)
            {
                ModelState.AddModelError(string.Empty, "Uživatel s tímto emailem nebo uživatelským jménem již existuje.");
                return View(user);
            }

            var uzivatel = new MusilZavěrečnýProjekt.Models.User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password // store plain password as requested
            };

            DbContext.users.Add(uzivatel);
            DbContext.SaveChanges();

            // Auto-login after registration: set TempData and redirect to reservations
            TempData["LoggedInUser"] = uzivatel.Username;
            TempData["Message"] = "Registrace proběhla úspěšně. Vítejte!";
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            TempData.Remove("LoggedInUser");
            TempData.Remove("Message");
            return RedirectToAction("Index", "Home");
        }
    }
}
