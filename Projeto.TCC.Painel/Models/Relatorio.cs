using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.TCC.Painel.Models
{
    [Table("Relatorios")]
    public class Relatorio
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório ")]
        public string Nome { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "O campo Mensagem é obrigatório ")]
        public string Mensagem { get; set; }

        public int AtributoId { get; set; }

        public virtual Atributo Atributo { get; set; }
    }
}