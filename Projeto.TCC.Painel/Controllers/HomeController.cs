using Projeto.TCC.Painel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Projeto.TCC.Painel.Controllers
{
    internal class RespostasFormulario
    {
        public int PerguntaId { get; set; }
        public int AtributoId { get; set; }
    }

    public class HomeController : Controller
    {

        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        private static List<RespostasFormulario> listRespostas = new List<RespostasFormulario>();        

        public ActionResult Index(bool erro = false)
        {
            if (erro)
            {
                @ViewBag.Mensagem = "Usuário ou Senha inválidos";
                erro = false;
            }

            Session.RemoveAll();
            return View();
        } 
       
        public ActionResult Sessao(string Nome, string Senha)
        {
            Usuario usuario = db.Usuarios.Where(w => w.Nome == Nome && w.Senha == Senha).FirstOrDefault();

            if(usuario != null)
            {
                Session["UsuarioId"] = usuario.Id;
                Session["Nome"] = usuario.Nome.ToString();
                Session["Email"] = usuario.Email.ToString();
                Session["QuestionarioId"] = Convert.ToInt32(usuario.QuestionarioId);
                Session["Questionario"] = usuario.Questionario.Nome.ToString();                

                return RedirectToAction("Perguntas");
            }
            else
            {
                return RedirectToAction("Index", new { erro = true });
            }
           
            
            
        }

        public ActionResult Perguntas()
        {
            Questionario questionario = db.Questionarios.Find((Session["QuestionarioId"]));
            questionario.Descricao = WebUtility.HtmlDecode(questionario.Descricao);
            ViewBag.QtdePerguntas = questionario.Perguntas.Count;
            return View(questionario);
        }        

        public void Respostas(int perguntaId, int atributoId)
        {
            var resposta = listRespostas.Where(w => w.PerguntaId == perguntaId).FirstOrDefault();

            if (resposta == null)
            {
                RespostasFormulario respostaFormulario = new RespostasFormulario();
                respostaFormulario.AtributoId = atributoId;
                respostaFormulario.PerguntaId = perguntaId;

                listRespostas.Add(respostaFormulario);
            }
            else
            {
                resposta.PerguntaId = perguntaId;
                resposta.AtributoId = atributoId;
            }           
        }

        public ActionResult Resultado()
        {
            InserirResultado();

            int usuarioId = Convert.ToInt32(Session["UsuarioId"]);

            var resultados = db.Resultados.Include(r => r.Usuario).Include(r => r.DetalhesResultado).Where(w => w.UsuarioId == usuarioId).FirstOrDefault();            

            List<Mensagem> listMensagem = new List<Mensagem>();

            int quantidadePerguntas = resultados.Usuario.Questionario.Perguntas.Count();

            var relatorios = db.Relatorios.ToList();

            foreach(var resultado in resultados.DetalhesResultado)
            {
                double porcentagem = (double) resultado.Quantidade / quantidadePerguntas;
                int atributoId = Convert.ToInt32(resultado.Atributo.Id);
                
                Mensagem mensagem = new Mensagem();
                mensagem.Porcentagem = ((Math.Round(porcentagem, 2))*100);
                mensagem.Descricao = WebUtility.HtmlDecode(relatorios.Where(w => w.AtributoId == atributoId).Select(s => s.Mensagem).FirstOrDefault().ToString());

                listMensagem.Add(mensagem);
            }

            return View(listMensagem);
    
        }

        public void InserirResultado()
        {
            var resultadoAtributo = listRespostas.GroupBy(g => new { g.AtributoId })
                                         .Select(g => new { g.Key.AtributoId, Count = g.Count() });

            List<DetalheResultado> detalhesResultado = new List<DetalheResultado>();

            foreach (var resultados in resultadoAtributo)
            {
                DetalheResultado detalheResultado = new DetalheResultado();
                detalheResultado.Atributo = db.Atributos.Find(resultados.AtributoId);
                detalheResultado.Quantidade = resultados.Count;
                detalhesResultado.Add(detalheResultado);
            }

            Resultado resultado = new Resultado()
            {
                UsuarioId = Convert.ToInt32(Session["UsuarioId"]),
                QuestionarioId = Convert.ToInt32(Session["QuestionarioId"]),
                DetalhesResultado = detalhesResultado

            };

            db.Resultados.Add(resultado);
            db.SaveChanges();          
        }
       
    }
}