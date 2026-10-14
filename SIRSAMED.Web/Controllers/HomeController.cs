using Microsoft.AspNetCore.Mvc;
using SIRSAMED.Web.Models;

namespace SIRSAMED.Web.Controllers
{
                   // Controlador principal encargado del acceso y navegacion
                   public class HomeController : Controller
    {
               // Redirige la pagina inicial hacia la pantalla de inicio de sesion
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

                     // Carga la interfaz visual del formulario de login (Peticion GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

                        // Procesa las credenciales y valida el acceso seguro (Peticion POST)
        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
                          // Valida que los campos obligatorios no esten vacios
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Debe completar todos los campos.";
                return View();
            }

                        // Aplica el algoritmo de hashing SHA-256 a la contrasena recibida
            string passwordHash = Seguridad.GenerarHash(password);

                      // Verifica las credenciales de prueba institucional con el hash generado
            if (correo == "admin@salud.gob.do" && Seguridad.ValidarHash("Admin123*", passwordHash))
            {
                return Content("Acceso autorizado a SIRSA MED. Hash verificado: " + passwordHash);
            }
            else
            {
                         // Manejo seguro de errores sin exponer informacion del servidor
                ViewBag.Error = "Credenciales incorrectas o usuario no registrado.";
                return View();
            }
        }
    }
}