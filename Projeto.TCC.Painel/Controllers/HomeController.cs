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
        public int PerfilId { get; set; }
    }

    public class HomeController : Controller
    {

        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        private static List<RespostasFormulario> listRespostas = new List<RespostasFormulario>();        

        public ActionResult Index(int erro = 0)
        {
            if (erro == 1)
            {
                @ViewBag.Mensagem = "Candidato ou Senha inválidos";
                erro = 0;
            }

            if (erro == 2)
            {
                @ViewBag.Mensagem = "Teste já realizado por este Candidato";
                erro = 0;
            }

            Session.RemoveAll();
            return View();
        } 
       
        public ActionResult Sessao(string Nome, string Senha)
        {
            Candidato Candidato = db.Candidatos.Where(w => w.Nome == Nome && w.Senha == Senha).FirstOrDefault();
            

            if(Candidato != null)
            {
                Resultado resultado = db.Resultados.Where(w => w.CandidatoId == Candidato.Id).FirstOrDefault();

                if (resultado == null)
                {
                    Session["CandidatoId"] = Candidato.Id;
                    Session["Nome"] = Candidato.Nome.ToString();
                    Session["Email"] = Candidato.Email.ToString();
                    Session["QuestionarioId"] = Convert.ToInt32(Candidato.QuestionarioId);
                    Session["Questionario"] = Candidato.Questionario.Nome.ToString();

                    return RedirectToAction("Perguntas");
                }
                else
                {
                    return RedirectToAction("Index", new { erro = 2 });
                }
            }
            else
            {
                return RedirectToAction("Index", new { erro = 1 });
            }
           
            
            
        }

        public ActionResult Perguntas()
        {
            Questionario questionario = db.Questionarios.Find((Session["QuestionarioId"]));
            questionario.Descricao = WebUtility.HtmlDecode(questionario.Descricao);
            ViewBag.QtdePerguntas = questionario.Perguntas.Count;
            return View(questionario);
        }        

        public void Respostas(int perguntaId, int PerfilId)
        {
            var resposta = listRespostas.Where(w => w.PerguntaId == perguntaId).FirstOrDefault();

            if (resposta == null)
            {
                RespostasFormulario respostaFormulario = new RespostasFormulario();
                respostaFormulario.PerfilId = PerfilId;
                respostaFormulario.PerguntaId = perguntaId;

                listRespostas.Add(respostaFormulario);
            }
            else
            {
                resposta.PerguntaId = perguntaId;
                resposta.PerfilId = PerfilId;
            }           
        }

        public ActionResult Resultado()
        {
            InserirResultado();

            int CandidatoId = Convert.ToInt32(Session["CandidatoId"]);

            var resultados = db.Resultados.Include(r => r.Candidato).Include(r => r.DetalhesResultado).Where(w => w.CandidatoId == CandidatoId).FirstOrDefault();            

            List<Mensagem> listMensagem = new List<Mensagem>();

            int quantidadePerguntas = resultados.Candidato.Questionario.Perguntas.Count();

            var relatorios = db.Relatorios.ToList();

            foreach(var resultado in resultados.DetalhesResultado)
            {
                double porcentagem = (double) resultado.Quantidade / quantidadePerguntas;
                int PerfilId = Convert.ToInt32(resultado.Perfil.Id);
                
                Mensagem mensagem = new Mensagem();
                mensagem.Porcentagem = ((Math.Round(porcentagem, 2))*100);
                mensagem.Descricao = WebUtility.HtmlDecode(relatorios.Where(w => w.PerfilId == PerfilId).Select(s => s.Mensagem).FirstOrDefault().ToString());
                mensagem.Perfil = resultado.Perfil.Titulo.ToString();

                listMensagem.Add(mensagem);
            }

            return View(listMensagem);
    
        }

        public void InserirResultado()
        {
            var resultadoPerfil = listRespostas.GroupBy(g => new { g.PerfilId })
                                         .Select(g => new { g.Key.PerfilId, Count = g.Count() });

            List<DetalheResultado> detalhesResultado = new List<DetalheResultado>();

            foreach (var resultados in resultadoPerfil)
            {
                DetalheResultado detalheResultado = new DetalheResultado();
                detalheResultado.Perfil = db.Perfils.Find(resultados.PerfilId);
                detalheResultado.Quantidade = resultados.Count;
                detalhesResultado.Add(detalheResultado);
            }

            Resultado resultado = new Resultado()
            {
                CandidatoId = Convert.ToInt32(Session["CandidatoId"]),
                QuestionarioId = Convert.ToInt32(Session["QuestionarioId"]),
                DetalhesResultado = detalhesResultado

            };

            db.Resultados.Add(resultado);
            db.SaveChanges();          
        }
       
    }
}