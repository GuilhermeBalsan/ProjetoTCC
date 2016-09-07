using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Projeto.TCC.Painel.Models
{
    public class ProjetoTCCPainelContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ProjetoTCCPainelContext()
            : base("name=ProjetoTCCPainelContext")
        {
        }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Questionario> Questionarios { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Pergunta> Perguntas { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Resposta> Respostas { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Atributo> Atributos { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Relatorio> Relatorios { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Administrador> Administradors { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Resultado> Resultados { get; set; }

        public System.Data.Entity.DbSet<Projeto.TCC.Painel.Models.Usuario> Usuarios { get; set; }
        
    }
}
