using ClearChoice.DAL;
using ClearChoice.Model;
using ClearChoice.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ClearChoice.Filters.AutenticacaoTipo;

namespace ClearChoice.Controllers
{
    public class MainController : Controller
    {
        [AutorizacaoTipo(new[] { TiposUsuarios.UsuarioPF })]
        public ActionResult PerfilUsuario()
        {
            return View();
        }

        // GET: Main
        [Authorize]
        public ActionResult Main()
        {
            return View();
        }
        [AutorizacaoTipo(new[] { TiposUsuarios.Administrador })]
        public ActionResult ListagemUsuariosPFView()
        {
            return View(CadastrarDAO.Listagem());
        }

        [AutorizacaoTipo(new[] { TiposUsuarios.Administrador })]
        public ActionResult ExperienciaView()
        {
            ViewBag.Categorias =
                  new SelectList(CategoriaDAL.Listagem(),
                  "ID", "Nome");

            if (ExperienciaDAO.VerificarVazio())
            {
                return View();
            }

            return RedirectToAction("ListaExperienciasView", "Main");
        }

        public ActionResult CadastrarExperienciaView()
        {
            ViewBag.Categorias =
                 new SelectList(CategoriaDAL.Listagem(),
                 "ID", "Nome");


            return View();
        }

        public ActionResult RemoverExperienciaView(int id)
        {

            if (ExperienciaDAO.BuscarExperienciaPorId(id) == null)
            {
                throw new ArgumentNullException("Objeto nulo!");
            }
            else
            {

                Experiencia ex = new Experiencia()
                {
                    ID = id
                };

                ExperienciaDAO.Remover(ex);
            }

            return RedirectToAction("ListaExperienciasView", "Main");
        }
        
        public ActionResult AlterarExperienciaView(int ID)
        {
            var experiencia = ExperienciaDAO.BuscarExperienciaPorId(ID);
             
            return View(experiencia);
        }
        [HttpPost]
        public ActionResult AlterarExperienciaView(Experiencia exp)
        {
            if (ModelState.IsValid)
            {
                ExperienciaDAO.Alterar(exp);
                return RedirectToAction("ListaExperienciasView", "Main");
            }
            return View(exp);
        }
        [Authorize]
        public ActionResult ListaExperienciasView(int? ID)
        {
            return View(ExperienciaDAO.BuscarProdutosPorCategoria(ID));
        }

        [HttpPost]
        public ActionResult Alterar(PessoaFisica pessoa)
        {
            CadastrarDAO.AlterarUsuario(pessoa);
            return RedirectToAction("ListagemUsuariosPFView", "Main");
        }

        [HttpPost]
        public ActionResult Remover(PessoaFisica pf)
        {

            ValidacaoLogin vl = new ValidacaoLogin()
            {
                Login = pf.Login
            };
            CadastrarDAO.RemoverUsuario(pf);

            if (!String.IsNullOrWhiteSpace(vl.urlRetorno) || Url.IsLocalUrl(vl.urlRetorno))
            {
                return Redirect(vl.urlRetorno);
            }

            if (pf.Login.Count() == 1)
            {
                return RedirectToAction("Home", "Home");
            }

            return RedirectToAction("Main", "Main");
        }

        [HttpPost]

        public ActionResult Cadastrar(Experiencia exp)
        {

            if (ModelState.IsValid)
            {
                var Exper = ExperienciaDAO.Cadastrar(exp);

                if (Exper)
                {
                    return RedirectToAction("ExperienciaView", "Main");
                }
            }
           

            return RedirectToAction("Main", "Main");
        }
    }
}