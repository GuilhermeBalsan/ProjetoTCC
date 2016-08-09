using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Perguntas")]
    public class Pergunta
    {
        public Pergunta()
        {
            this.Respostas = new List<Resposta>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório ")]
        public string Titulo { get; set; }

        public int QuestionarioId { get; set; }

        public virtual Questionario Questionario { get; set; }

        public virtual ICollection<Resposta> Respostas { get; set; }

    }
}