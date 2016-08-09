using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto.TCC.Painel.Models;

namespace Projeto.TCC.Painel.Controllers
{
    [Authorize]
    public class RespostasController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Respostas/
        public ActionResult Index(int? perguntaId)
        {

            if (perguntaId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var respostas = db.Respostas.Include(r => r.Atributo).Include(r => r.Pergunta).Where(w => w.PerguntaId == perguntaId).ToList();               

            var pergunta = db.Perguntas.Where(w => w.Id == perguntaId).ToList();            

            ViewBag.Pergunta = pergunta.Select(s => s.Titulo).FirstOrDefault();
            ViewBag.PerguntaId = pergunta.Select(s => s.Id).FirstOrDefault();
            ViewBag.QuestionarioId = pergunta.Select(s => s.QuestionarioId).FirstOrDefault();
           

            return View(respostas);
        }

        // GET: /Respostas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resposta resposta = db.Respostas.Find(id);
            if (resposta == null)
            {
                return HttpNotFound();
            }
            return View(resposta);
        }

        // GET: /Respostas/Create
        public ActionResult Create(int? perguntaId)
        {
            if (perguntaId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo");
            ViewBag.PerguntaId = perguntaId;
            return View();
        }

        // POST: /Respostas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titulo,PerguntaId,AtributoId")] Resposta resposta)
        {
            if (ModelState.IsValid)
            {
                db.Respostas.Add(resposta);
                db.SaveChanges();
                return RedirectToAction("Index", new { perguntaId = resposta.PerguntaId });
            }

            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", resposta.AtributoId);
            ViewBag.PerguntaId = resposta.PerguntaId;
            return View(resposta);
        }

        // GET: /Respostas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resposta resposta = db.Respostas.Find(id);
            if (resposta == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", resposta.AtributoId);
            ViewBag.PerguntaId = new SelectList(db.Perguntas, "Id", "Titulo", resposta.PerguntaId);
            return View(resposta);
        }

        // POST: /Respostas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titulo,PerguntaId,AtributoId")] Resposta resposta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resposta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { perguntaId = resposta.PerguntaId });
            }
            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", resposta.AtributoId);
            ViewBag.PerguntaId = new SelectList(db.Perguntas, "Id", "Titulo", resposta.PerguntaId);
            return View(resposta);
        }

        // GET: /Respostas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resposta resposta = db.Respostas.Find(id);
            if (resposta == null)
            {
                return HttpNotFound();
            }
            return View(resposta);
        }

        // POST: /Respostas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resposta resposta = db.Respostas.Find(id);
            db.Respostas.Remove(resposta);
            db.SaveChanges();
            return RedirectToAction("Index", new { perguntaId = resposta.PerguntaId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
