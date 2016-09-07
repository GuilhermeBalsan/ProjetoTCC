using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public int QuestionarioId { get; set; }

        public virtual Questionario Questionario { get; set; }

    }
}