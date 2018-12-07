using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClearChoice.Model
{
    [Table("Categorias")]
    public class Categoria:BaseModel
    {
        public string Nome { get; set; }
       
    }
}