using ApplicazioneAlbergo_core_Entity.data;
using ApplicazioneAlbergo_core_Entity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApplicazioneAlbergo_core_Entity.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        public LoginController(ApplicationDbContext db, IAuthenticationSchemeProvider schemeProvider)
        {
            _db = db;
            _schemeProvider = schemeProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Dipendente dipendente)
        {
            if (dipendente.NomeUtente != null && dipendente.Password != null)
            {
                //string sqlQuery = "SELECT * FROM Dipendenti" +
                //    " WHERE NomeUtente = @nome AND Password = @password";

                string sqlQuery = "SELECT * FROM Dipendenti" +
                    " WHERE NomeUtente = @nome AND Password = @password";

                var nomeParam = new SqlParameter("@nome", dipendente.NomeUtente);
                var passwordParam = new SqlParameter("@password", dipendente.Password);

                var user = await _db.Dipendenti.FromSqlRaw(sqlQuery, nomeParam, passwordParam).FirstOrDefaultAsync();

                if (user != null)
                {
                    if (user.NomeUtente == dipendente.NomeUtente && user.Password == dipendente.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.NomeUtente),
                            new Claim(ClaimTypes.Role, "Admin")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties();

                        await HttpContext.SignInAsync(
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       new ClaimsPrincipal(claimsIdentity),
                       authProperties);

                        TempData["Message"] = "Login effettuato con successo";
                        return RedirectToAction("Index");
                    }
                }
                TempData["Errore"] = "Lo User è nullo.";
                return RedirectToAction("Index", "Home");

            }
            TempData["Errore"] = "Username o password inseriti male.";
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["Message"] = "Logout effettuato.";
            return RedirectToAction("Index", "Home");
        }
    }
}
