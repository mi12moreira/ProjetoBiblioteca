using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Biblioteca.Models;
using Microsoft.AspNetCore.Http;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuarios p)
        {
            UsuarioRepository user = new UsuarioRepository();
            Usuarios usuario = user.ValidarLogin(p);

            if(usuario.id != 0)
            {
                 HttpContext.Session.SetInt32("id", usuario.id);
                HttpContext.Session.SetString("login", usuario.login);
                HttpContext.Session.SetString("privilegio", usuario.privilegio);

                int idUsuario = (int)HttpContext.Session.GetInt32("id");
                return Redirect("Index");
            }
            else
            {   ViewData["Erro"] = "Senha inválida";
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
