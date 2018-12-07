using ClearChoice.Model;
using System.ComponentModel.DataAnnotations;


namespace ClearChoice.ViewModel
{
    public class ValidacaoPessoa
    {
        [Required(ErrorMessage = "Informe o seu Email!")]
        [MaxLength(100, ErrorMessage = "Limite de 100 Caracteres")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o seu Login")]
        [MaxLength(50, ErrorMessage = "Limite de 50 caracteres")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a sua Senha")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "A senha deve ter pelo menos 8 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o seu RG")]
        [MaxLength(50, ErrorMessage = "Limite de 9 caracteres")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Informe o seu Nome")]
        [MaxLength(60, ErrorMessage = "Excedeu o limite de caractere")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a Data de Nascimento")]
        [DataType(DataType.Date)]
        public string DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe a sua Cidade")]
        [MaxLength(60, ErrorMessage = "Excedeu o limite de caractere")]
        public string Localidade { get; set; }

        [Required(ErrorMessage = "Informe a sigla do seu Estado")]
        [MaxLength(2, ErrorMessage = "Excedeu o limite de caractere")]
        public string UF { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [MaxLength(12, ErrorMessage = "Excedeu o limite de caractere")]
        public string CEP { get; set; }

        public Categoria Categoria { get; set; }
    }
}