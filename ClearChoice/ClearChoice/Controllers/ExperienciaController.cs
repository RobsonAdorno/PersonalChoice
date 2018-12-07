using ClearChoice.DAL;
using ClearChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ClearChoice.Controllers
{
   
    public class ExperienciaController : Controller
    {
        // GET: Experiencia
        [Authorize]

        public ActionResult DetalheExperiencia(int ID)
        {
          
                if (ID != null)
                {
                    var verificadorExper = ExperienciaDAO.BuscarExperienciaPorId(ID);

                    TempData["id"] = ID;
                
                    if (verificadorExper != null)
                    {
                        return View(verificadorExper);
                    }

                    return RedirectToAction("ListaExperienciaView", "Main");
                }


                return RedirectToAction("ListaExperienciaView", "Main");
            }

        
        [HttpPost]
        [Authorize]
        public ActionResult DetalheExperiencia(Experiencia exp, PessoaFisica pf)
        {
            if (User.Identity.IsAuthenticated)
            {
               var login = ((ClaimsIdentity)User.Identity).FindFirst("Login");

                pf.Login = Convert.ToString(login.Value);
                
              var pessoaFisica = CadastrarDAO.BuscarPessoaLogin(pf);

                pf = (PessoaFisica)pessoaFisica;

              

            }
            else
            {
                return RedirectToAction("Home", "Home");
            }

            if (TempData["id"] != null)
            {
             exp  =  ExperienciaDAO.BuscarExperienciaPorId(Convert.ToInt16(TempData["id"]));
               
               
                if (pf.Experiencia == null)
                {
                    exp.estaHabilitado = false;
                    pf.Experiencia = exp;
                    CadastrarDAO.AlterarUsuarioParaExperiencia(pf);
                    var expe = ExperienciaDAO.Alterar(exp);

                    if (expe)
                    {
                        return RedirectToAction("Main", "Main");
                    }
                }
                return RedirectToAction("ErroExperiencia", "Experiencia");

            }

            return RedirectToAction("Main", "Main");

        }
        [Authorize]
        public ActionResult ErroExperiencia()
        {

            return View();
        }
    }
}