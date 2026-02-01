using Microsoft.AspNetCore.Mvc;
using MusilZavěrečnýProjekt.Models;

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
            User uzivatel = new User();

            uzivatel.Username = user.Username;
            uzivatel.Email = user.Email;
            uzivatel.PasswordHash = user.Password;

            DbContext.users.Add(uzivatel);
            DbContext.SaveChanges();

            return View("Index");
        }
    }
}
