using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("DetalhesResultado")]
    public class DetalheResultado
    {
        public int Id { get; set; }

        public Atributo Atributo { get; set; }

        public int Quantidade { get; set; }        
    }
}