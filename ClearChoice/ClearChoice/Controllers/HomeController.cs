using ClearChoice.DAL;
using ClearChoice.Model;
using ClearChoice.Utils;
using ClearChoice.ViewModel;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ClearChoice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ValidacaoLogin viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Login", "Login incorreto!");
                return View();
            }


            PessoaFisica pf = new PessoaFisica()
            {
                Login = viewModel.Login,
                Senha = viewModel.Senha

            };

            var senha = Cryptography.Crypto(pf.Senha);
            var usuarios = CadastrarDAO.BuscarPessoaLogin(pf);

            if (!usuarios.TipoUsuario.Equals("UsuarioPJ")) {

                if (usuarios != null && usuarios.Senha.Equals(senha))
                {
                    var identityPF = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, usuarios.Nome),
                    new Claim("Login", usuarios.Login),
                    new Claim(ClaimTypes.Role, usuarios.TipoUsuario.ToString())
                }, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(identityPF);

                    if (!String.IsNullOrWhiteSpace(viewModel.urlRetorno) || Url.IsLocalUrl(viewModel.urlRetorno))
                    {
                        return Redirect(viewModel.urlRetorno);
                    }

                    return RedirectToAction("Main", "Main");

                }

                ModelState.AddModelError("Login", "Login Incorreto!");
            }

            TempData["error"] = "Já entraremos em contato para liberar o acesso!\nAgradeçemos a preferência!";

            return View();
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Home", "Home");
        }
       
        [HttpGet]
        public ActionResult EnvioEmailView(string email)
        {
            
            if (email != null)
            {

                Pessoa pessoa = new Pessoa()
                {
                    Email = email
                };

                var ConfimarSenha = CadastrarDAO.MudarSenha(pessoa);

                if (ConfimarSenha && ModelState.IsValid)
                {
                    TempData["success"] = "Nova senha encaminhada para " + email;
                    return RedirectToAction("Login", "Home");
                }else
                {
                    TempData["error"] = "Email inválido!!";
                    return RedirectToAction("Login", "Home");
                }
               
            }
            TempData["error"] = "Email inválido!!";
            return RedirectToAction("Login", "Home");

        }
    
        }
    }
