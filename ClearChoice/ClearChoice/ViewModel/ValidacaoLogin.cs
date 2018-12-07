using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClearChoice.ViewModel
{
    public class ValidacaoLogin
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Limite de 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres")]
        public string Senha { get; set; }

       
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres")]
        public string SenhaAtual { get; set; }

        [HiddenInput]
        public string urlRetorno { get; set; }
    }
}