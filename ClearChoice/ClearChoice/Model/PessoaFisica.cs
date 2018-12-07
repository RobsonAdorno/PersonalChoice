using System.ComponentModel.DataAnnotations;



namespace ClearChoice.Model
{
    public class PessoaFisica : Pessoa
    {
        public string RG { get; set; }
        public string DataNascimento { get; set; }
        public bool estaPermitidoFisica { get; set; } = true;
        public Experiencia Experiencia { get; set; }
        public Categoria Categoria { get; set; }
        public TiposUsuarios TipoUsuario { get; set; } = TiposUsuarios.UsuarioPF;
    }
}