using ClearChoice.Model;
using System.ComponentModel.DataAnnotations;

namespace ClearChoice.ViewModel
{
    public class ValidacaoPessoaJuridica:ValidacaoPessoa
    {
        [Required(ErrorMessage = "Informe o CNPJ")]
        [MaxLength(12, ErrorMessage = "Excedeu o limite de caractere")]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o Endereço")]
        [MaxLength(50, ErrorMessage = "Excedeu o limite de caractere")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe o Telefone")]
        [MaxLength(50, ErrorMessage = "Excedeu o limite de caractere")]
        public string Telefone { get; set; }
    }
}