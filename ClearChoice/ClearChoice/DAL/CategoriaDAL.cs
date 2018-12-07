using ClearChoice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClearChoice.DAL
{
    public class CategoriaDAL
    {
        private static Context ctx = SingletonContext.GetContext();

        public static Categoria BuscarCategoriaPorNome(Categoria categoria)
        {
            return ctx.Categorias.FirstOrDefault(x => x.Nome.Equals(categoria.Nome));
        }

        public static Categoria BuscarCategoriaPorID(int? ID)
        {
            return ctx.Categorias.FirstOrDefault(x => x.ID == ID);
        }

        public static bool CadastrarCategoria(Categoria categoria)
        {
            if (BuscarCategoriaPorNome(categoria) == null)
            {
                ctx.Categorias.Add(categoria);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

       public static List<Categoria> Listagem()
        {
            return ctx.Categorias.ToList();
        }
    }
}