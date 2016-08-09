using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Administradores")]
    public class Administrador
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório ")]
        public string Nome { get; set; }

        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}