using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClearChoice.Model
{
    public class Autenticacao:BaseModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        [HiddenInput]
        public string TipoDeUsuario { get; set; }
    }
}