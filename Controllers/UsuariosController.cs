using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers{
    public class UsuariosController : Controller
    {
        public IActionResult Cadastro(){
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("privilegio") != "adm")
            {
                TempData["SemAcesso"] = "Você não possui esse nível de acesso!";
                return RedirectToAction("Login", "Home");

            }
            else {
            return View("Cadastro");
            }
        }

        [HttpPost]
        public IActionResult Cadastro(Usuarios u){

            UsuarioRepository usr = new UsuarioRepository();
            usr.Inserir(u);
            return RedirectToAction("Listar");
        }

        public IActionResult Listar()
        {
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            UsuarioRepository usr = new UsuarioRepository();
            return View(usr.Listar());
        }

        public IActionResult Excluir(int id)
        {
            UsuarioRepository usr = new UsuarioRepository();
            usr.Deletar(id);
            return RedirectToAction("Listar");
        }

        public IActionResult Editar(int id)
        {
            if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login");
            }
            UsuarioRepository usr = new UsuarioRepository();
            return View(usr.BuscarPorId(id));
        }

        [HttpPost]
        public IActionResult Editar(Usuarios UserRep)
        {
            UsuarioRepository usr = new UsuarioRepository();
            usr.Editar(UserRep);
            return RedirectToAction("Listar");
        }
    }
} 