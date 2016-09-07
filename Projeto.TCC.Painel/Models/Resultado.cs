﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Resultados")]
    public class Resultado
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int QuestionarioId { get; set; }
        

        //public virtual Questionario Questionario { get; set; }

        public List<DetalheResultado> DetalhesResultado { get; set; }

    }
}