using Microsoft.AspNetCore.Mvc;
using SIRSAMED.Web.Models;

namespace SIRSAMED.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe completar todos los campos.";
                return View();
            }

            string passwordHash = Seguridad.GenerarHash(password);

            if (correo == "admin@salud.gob.do" && Seguridad.ValidarHash("Admin123*", passwordHash))
            {
                return Content("Acceso autorizado a SIRSA MED. Hash verificado: " + passwordHash);
            }
            else
            {
                ViewBag.Error = "Credenciales incorrectas o usuario no registrado.";
                return View();
            }
        }
    }
}