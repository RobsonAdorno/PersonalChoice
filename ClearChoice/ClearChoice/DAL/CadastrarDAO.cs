using ClearChoice.Model;
using ClearChoice.Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClearChoice.DAL
{
    public class CadastrarDAO
    {
        private static Context ctx = SingletonContext.GetContext();

        //------------------------------------------------------------- Daqui p baixo é apenas transações com pessoas fisicas ------------------------

        public static Pessoa BuscarPessoaLogin(PessoaFisica pf)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Login.Equals(pf.Login));
        }

        public static Pessoa BuscarSenha(string senha)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Senha.Equals(senha));
        }


        public static Pessoa BuscarPessoaId(int id)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.ID == id);
        }

        public static Pessoa BuscarPessoaEmail(string email)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Email.Equals(email));
        }

        public static bool CadastrarPF(PessoaFisica pf)
        {
            if (BuscarPessoaLogin(pf) != null && BuscarPessoaEmail(pf.Email) != null)
            {
                return false;
            }

            ctx.Pessoas.Add(pf);
            ctx.SaveChanges();
            return true;
        }

        public static List<PessoaFisica> Listagem()
        {
            return ctx.Pessoas.OfType<PessoaFisica>().ToList();
        }

        public static void AlterarUsuario(PessoaFisica pessoa)
        {
            var newItem = BuscarPessoaId(pessoa.ID);

            pessoa = (PessoaFisica) newItem;

            ctx.Entry(newItem).State = EntityState.Detached;
            ctx.Entry(pessoa).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public static void AlterarUsuarioParaExperiencia(PessoaFisica pessoa)
        {
            
            ctx.Entry(pessoa).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public static void RemoverUsuario(PessoaFisica pf)
        {
            ctx.Pessoas.Remove(BuscarPessoaId(pf.ID));
            ctx.SaveChanges();
        }

        public static bool MudarSenha(Pessoa pessoa)
        {
                pessoa = BuscarPessoaEmail(pessoa.Email);

                if (pessoa != null)
                {
                    var NovaSenha = GeneratePassword.Generate();
                    EnvioEmail.Enviar(pessoa.Email, NovaSenha);
                    pessoa.Senha = Cryptography.Crypto(NovaSenha);

                    // ctx.Entry(Email).State = EntityState.Detached;
                    ctx.Entry(pessoa).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
            return false;
        }
            
           
        

        //----------------------------------------------- Daqui p baixo é apenas pessoas juridicas ------------------------------------------------

        public static Pessoa VerificarPJLogin(PessoaJuridica pj)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Login.Equals(pj.Login));
        }

        public static Pessoa VerificarPJPorEmail(PessoaJuridica pj)
        {
            return ctx.Pessoas.FirstOrDefault(x => x.Email.Equals(pj.Email));
        }

        public static List<PessoaJuridica> ListagemJuridica()
        {
            return ctx.Pessoas.OfType<PessoaJuridica>().ToList();
        }

        public static bool CadastrarPJ(PessoaJuridica pj)
        {
            if (VerificarPJLogin(pj) != null && VerificarPJPorEmail(pj) != null)
            {
                return false;
            }
            
            ctx.Pessoas.Add(pj);
            ctx.SaveChanges();
            return true;
        }
        //---------------------------------------------------------------- Daqui p baixo é apenas Login ---------------------------------------------

       public static void RegistraLogin(Autenticacao autenticar)
        {
           
            ctx.Autenticar.Add(autenticar);
            ctx.SaveChanges();
        }

        public static void AlterarSenha(Pessoa p)
        {
            ctx.Entry(p).State = System.Data.Entity.EntityState.Modified;
            ctx.SaveChanges();
        }
        
    }
}