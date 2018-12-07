using ClearChoice.DAL;
using ClearChoice.Model;
using ClearChoice.Utils;
using ClearChoice.ViewModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;

namespace ClearChoice.Controllers
{
    public class CadastrarController : Controller
    {
        private object viewModel;

        // GET: Cadastrar

        [HttpPost]
        public ActionResult BuscarCep(ValidacaoPessoa pessoa)
        {

            if (pessoa.CEP != null) {
                //Criar a URL para requisição
                string url = "https://viacep.com.br/ws/" + pessoa.CEP + "/json/";
                
                //Criar para fazer o download do JSON
                WebClient client = new WebClient();
                //Fazer o download do JSON
                try
                {
                    string resultado = client.DownloadString(url);
                    //Criptografar para UTF8
                    byte[] bytes = Encoding.Default.GetBytes(resultado);
                    resultado = Encoding.UTF8.GetString(bytes);
                    //Converter o JSON para o ojeto
                    pessoa = JsonConvert.DeserializeObject<ValidacaoPessoa>(resultado);
                    TempData["Usuario"] = pessoa;
                    return RedirectToAction("CadastrarPFView", "Cadastrar");
                }
                catch (Exception)
                {
                    TempData["error"] = "CEP inválido!";
                    return RedirectToAction("CadastrarPFView", "Cadastrar");
                }
                
            }


            TempData["errorCEP"] = "CEP nulo";
            return RedirectToAction("CadastrarPFView", "Cadastrar");
        }

        [HttpPost]
        public ActionResult BuscarCepPessoaJuridica(ValidacaoPessoaJuridica pessoa)
        {

            if (pessoa.CEP != null)
            {
                //Criar a URL para requisição
                string url = "https://viacep.com.br/ws/" + pessoa.CEP + "/json/";

                //Criar para fazer o download do JSON
                WebClient client = new WebClient();
                //Fazer o download do JSON
                try
                {
                    string resultado = client.DownloadString(url);
                    //Criptografar para UTF8
                    byte[] bytes = Encoding.Default.GetBytes(resultado);
                    resultado = Encoding.UTF8.GetString(bytes);
                    //Converter o JSON para o ojeto
                    pessoa = JsonConvert.DeserializeObject<ValidacaoPessoaJuridica>(resultado);
                    TempData["Usuario"] = pessoa;
                    return RedirectToAction("CadastrarPJView", "Cadastrar");
                }
                catch (Exception)
                {
                    TempData["error"] = "CEP inválido!";
                    return RedirectToAction("CadastrarPJView", "Cadastrar");
                }

            }


            TempData["errorCEP"] = "CEP nulo";
            return RedirectToAction("CadastrarPJView", "Cadastrar");
        }

        public ActionResult CadastrarPFView()
        {
            ViewBag.Categorias =
                 new SelectList(CategoriaDAL.Listagem(),
                 "ID", "Nome");

            if (TempData["errorCEP"] != null)
            {
                ModelState.AddModelError("CEP", "Campo CEP é obrigatório");
                return View();
            }else if (TempData["error"] != null)
            {
           
                return View();
            }

            return View(TempData["Usuario"]);
        }

        [HttpPost]

        public ActionResult CadastrarPFView(ValidacaoPessoa vp, int? Categorias)
        {
            ViewBag.Categorias =
                  new SelectList(CategoriaDAL.Listagem(),
                  "ID", "Nome");

            vp.Categoria = CategoriaDAL.BuscarCategoriaPorID(Categorias);

            PessoaFisica pf = new PessoaFisica()
            {
                Nome = vp.Nome,
                Email = vp.Email,
                Login = vp.Login,
                RG = vp.RG,
                DataNascimento = vp.DataNascimento,
                Localidade = vp.Localidade,
                UF = vp.UF,
                CEP = vp.CEP,
                Categoria = vp.Categoria,
                Senha = Cryptography.Crypto(vp.Senha)
            };

            
            if (CadastrarDAO.CadastrarPF(pf) && ModelState.IsValid)
            {
                TempData["sucess"] = "Cadastro Efetuado com sucesso!";
                return RedirectToAction("Login", "Home");

            }
            TempData["error"] = "Erro ao cadastrar!";
            return View();
            //drop and down
        }

        [HttpPost]
        public ActionResult CadastrarPJView(ValidacaoPessoaJuridica vp)
        {
            PessoaJuridica pj = new PessoaJuridica()
            {
                CNPJ = vp.CNPJ,
                Nome = vp.Nome,
                Email = vp.Email,
                Login = vp.Login,
                Localidade = vp.Localidade,
                Logradouro = vp.Logradouro,
                UF = vp.UF,
                CEP = vp.CEP,
                Telefone = vp.Telefone,
                Senha = Cryptography.Crypto(vp.Senha)
            };

            if (CadastrarDAO.CadastrarPJ(pj))
            {
                TempData["sucess"] = "Cadastro Efetuado com sucesso!";
                return RedirectToAction("Login", "Home");

            }
            TempData["error"] = "Erro ao cadastrar!";
            return View();
        }

        public ActionResult CadastrarPJView()
        {
            
            if (TempData["errorCEP"] != null)
            {
                ModelState.AddModelError("CEP", "Campo CEP é obrigatório");
                return View();
            }
            else if (TempData["error"] != null)
            {

                return View();
            }

            return View(TempData["Usuario"]);
            
        }

        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(ValidacaoLogin viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(x => x.Type == "Login").Value;
            Pessoa p = new Pessoa()
            {
                Login = viewModel.Login
            };

           // var cadastrar = CadastrarDAO.BuscarPessoaLogin(p);

            if (Cryptography.Crypto(viewModel.SenhaAtual) != p.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha errada!");
                return View();
            }

            CadastrarDAO.AlterarSenha(p);

            TempData["success"] = "Senha alterada com sucesso!";
            return RedirectToAction("Main", "Main");
        }

        [HttpGet]
        public ActionResult DetalhesUsuarioView(int id)
        {
            var usuario = CadastrarDAO.BuscarPessoaId(id);

            // return Json(usuario, JsonRequestBehavior.AllowGet);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Alterar(PessoaFisica pessoa)
        {
            CadastrarDAO.AlterarUsuario(pessoa);
            return RedirectToAction("ListagemUsuariosPFView", "Cadastrar");
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

            return RedirectToAction("Home", "Home");
        }
    }
}