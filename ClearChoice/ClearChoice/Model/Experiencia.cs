using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClearChoice.Model
{
	[Table("Experiencias")]
    public class Experiencia:BaseModel
    {
        public Experiencia()
        {
            Categoria = new Categoria();
        }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Nome da Experiência")]
        public string NomeExperiencia { get; set; }
        public Categoria Categoria { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        
        public string Avaliacao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        public bool estaHabilitado { get; set; }
    }
}