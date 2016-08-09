using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Projeto.TCC.Painel.Models
{
    [Table("Questionarios")]
    public class Questionario
    {
        public Questionario()
        {
            this.Perguntas = new List<Pergunta>();
        }

        public int Id { get; set; }

        [Required( ErrorMessage = "O campo Nome é obrigatório ")]
        public string Nome { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "O campo Descricao é obrigatório ")]
        public string Descricao { get; set; }

        public virtual ICollection<Pergunta> Perguntas { get; set; }

    }
}