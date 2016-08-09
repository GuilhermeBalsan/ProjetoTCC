using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Respostas")]
    public class Resposta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório ")]
        public string Titulo { get; set; }

        public int PerguntaId { get; set; }

        public virtual Pergunta Pergunta { get; set; }

        public int AtributoId { get; set; }

        public virtual Atributo Atributo { get; set; }
    }
}