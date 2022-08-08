using Biblioteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        public IActionResult Cadastro()
        {
           if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Home");
            }
           else
           {
            return View();
           }
        }

        [HttpPost]
        public IActionResult Cadastro(Livro l)
        {
            LivroService livroService = new LivroService();

            if (l.Id == 0)
            {
                livroService.Inserir(l);
            }
            else
            {
                livroService.Atualizar(l);
            }

            return RedirectToAction("Listagem");
        }

        public IActionResult Listagem(string tipoFiltro, string filtro)
        {
          if (HttpContext.Session.GetInt32("id") == 0 || HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Login", "Home");
            }

        else
            {
                System.Console.WriteLine("Tipo Filtro " + tipoFiltro);
                FiltrosLivros objFiltro = null;
                if (!string.IsNullOrEmpty(filtro))
                {
                    objFiltro = new FiltrosLivros();
                    objFiltro.Filtro = filtro;
                    objFiltro.TipoFiltro = tipoFiltro;
                }

                LivroService livroService = new LivroService();
                return View(livroService.ListarTodos(objFiltro));
            }
        }

        public IActionResult Edicao(int id)
        {
            Autenticacao.CheckLogin(this);
            LivroService ls = new LivroService();
            Livro l = ls.ObterPorId(id);
            return View(l);
        }
    }
}