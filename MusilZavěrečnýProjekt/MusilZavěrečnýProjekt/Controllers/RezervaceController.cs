using Microsoft.AspNetCore.Mvc;
using MusilZavěrečnýProjekt.Models;
using System.Linq;
using System;

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
            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            // Najdeme existujícího uživatele podle e-mailu nebo jména
            var existing = DbContext.users.FirstOrDefault(u => u.Email == user.Email || u.Username == user.Username);

            if (existing != null)
            {
                // Ověříme heslo (v produkci používejte hash a bezpečné porovnání)
                if (existing.PasswordHash == user.Password)
                {
                    TempData["Message"] = $"Přihlášen: {existing.Username}";
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Nesprávné heslo.");
                return View(user);
            }

            // Pokud uživatel neexistuje, vytvoříme nový (pokud nechcete automatickou registraci, změňte sem chování)
            var uzivatel = new User
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.Password // Pozn.: pro produkci hashe
            };

            DbContext.users.Add(uzivatel);
            try
            {
                DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Chyba při ukládání: " + ex.Message);
                return View(user);
            }

            TempData["Message"] = "Účet vytvořen a přihlášen.";
            return RedirectToAction("Index");
        }
    }
}
