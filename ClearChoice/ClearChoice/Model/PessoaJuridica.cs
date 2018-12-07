using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClearChoice.Model
{
    public class PessoaJuridica:Pessoa
    {
        
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        
        public TiposUsuarios TipoUsuario { get; set; } = TiposUsuarios.UsuarioPJ;

    }
}