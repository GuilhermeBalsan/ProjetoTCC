﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Projeto.TCC.Painel.Models
{
    [Table("Perfis")]
    public class Perfil
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Titulo é obrigatório ")]
        public string Titulo { get; set; }        
        
    }
}