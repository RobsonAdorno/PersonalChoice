using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClearChoice.Model
{
    public class Context : DbContext
    {

        public Context() : base("PersonalChoice")
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Autenticacao> Autenticar { get; set; }

        public DbSet<Experiencia> Experiencias { get; set; }

        public DbSet<Categoria> Categorias { get; set; }
    }
}