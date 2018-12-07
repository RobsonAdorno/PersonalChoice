using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ClearChoice.Model;

namespace ClearChoice.Model
{
    [Table("Pessoas")]
    public class Pessoa : BaseModel
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Localidade { get; set; }
        public TiposUsuarios TipoUsuario { get; set; } = TiposUsuarios.UsuarioPF;
        public string Logradouro { get; set; }
        



    }
}