using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("PesosQuestionarios")]
    public class PesoQuestionario
    {        

        public int Id { get; set; }

        public int QuestionarioId { get; set; }

        public virtual Questionario Questionarios { get; set; }

        public int PerfilId { get; set; }

        public virtual Perfil Perfils { get; set; }

        public int Peso { get; set; }

    }
}