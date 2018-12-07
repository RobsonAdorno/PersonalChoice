using ClearChoice.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClearChoice.DAL
{
    public class ExperienciaDAO
    {
        private static Context ctx = SingletonContext.GetContext();


        public static Experiencia VerificadorExperiencia(Experiencia exp)
        {
            return ctx.Experiencias.FirstOrDefault(x => x.ID.Equals(exp.ID));
        }

        public static Experiencia BuscarExperienciaPorId(int id)
        {
            return ctx.Experiencias.FirstOrDefault(x => x.ID == id);
        }

        public static List<Experiencia> BuscarProdutosPorCategoria(int? id)
        {
            if (id == null)
            {
                return ctx.Experiencias.ToList();
            }

            return ctx.Experiencias.Include("Categoria").
                Where(x => x.Categoria.ID == id).ToList();
        }

        public static List<Experiencia> RetornarProduto()
        {
            return ctx.Experiencias.Include("Categoria").ToList();
        }

        public static bool VerificarVazio()
        {
            if (ctx.Experiencias.Count() == 0){

                return true;
            }

            return false;
        }

        public static bool Cadastrar(Experiencia exp)
        {
            if (BuscarExperienciaPorId(exp.ID) != null)
            {
                return false;
            }

            ctx.Experiencias.Add(exp);
            ctx.SaveChanges();
            return true;
        }

        public static void Remover(Experiencia exp)
        {
            ctx.Experiencias.Remove(BuscarExperienciaPorId(exp.ID));
            ctx.SaveChanges();
        }

        public static bool Alterar(Experiencia exp)
        {
            if (exp != null)
            {
                var experienciaID = BuscarExperienciaPorId(exp.ID);

                ctx.Entry(experienciaID).State = EntityState.Detached;
                ctx.Entry(exp).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }

            return false;
        }

        public static List<Experiencia> Listagem()
        {
            
            return ctx.Experiencias.ToList();
        }
        
    }
}