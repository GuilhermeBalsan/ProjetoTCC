using Projeto.TCC.Painel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Index()
        {
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome");
            Session.RemoveAll();
            return View();
        } 
       
        public ActionResult Sessao(string nome, string email, int QuestionarioId)
        {
            Session["Nome"] = nome;
            Session["Email"] = email;
            Session["QuestionarioId"] = QuestionarioId;
            Session["Questionario"] = db.Questionarios.Where(w => w.Id == QuestionarioId).Select(s => s.Nome).FirstOrDefault();
            
            return RedirectToAction("Perguntas");
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
            int resultadoAtributo = listRespostas.GroupBy(g => new { g.AtributoId })
                                         .OrderByDescending(o => o.Count())
                                         .Take(1)
                                         .Select(s => s.Key.AtributoId)
                                         .First();

            InserirResultado(resultadoAtributo);

            var relatorio = WebUtility.HtmlDecode(db.Relatorios.Where(w => w.AtributoId == resultadoAtributo).Select(s => s.Mensagem).FirstOrDefault());
            ViewBag.Relatorio = relatorio;

            return View ();
    
        }

        public void InserirResultado(int atributoId)
        {
            Resultado resultado = new Resultado() 
            { 
                AtributoId = atributoId,
                Email = Session["Email"].ToString(),
                Nome = Session["Nome"].ToString(),                
                QuestionarioId = Convert.ToInt32(Session["QuestionarioId"])
                
            };
            
            db.Resultados.Add(resultado);
            db.SaveChanges();                        
        }
    }
}

/*
 qual atributo mais apareceu
 var mostFollowedQuestions = context.UserIsFollowingQuestion
                                    .GroupBy(q => q.QuestionId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(5)
                                    .Select(g => g.Key).ToList();
 */