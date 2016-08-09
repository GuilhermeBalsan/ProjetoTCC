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
    public class PerguntasController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Perguntas/
        public ActionResult Index(int questionarioId)
        {
            var perguntas = db.Perguntas.Include(p => p.Questionario).Where(w => w.QuestionarioId == questionarioId).ToList();

            ViewBag.Questionario = db.Questionarios.Where(w => w.Id == questionarioId).Select(s => s.Nome).FirstOrDefault();
            ViewBag.QuestionarioId = questionarioId;           

            //ViewBag.Questionarios = new SelectList(db.Questionarios, "Id", "Nome");

            return View(perguntas.ToList());
        }

        // GET: /Perguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pergunta pergunta = db.Perguntas.Find(id);
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            return View(pergunta);
        }

        // GET: /Perguntas/Create
        public ActionResult Create(int questionarioId)
        {
            ViewBag.QuestionarioId = questionarioId;
            return View();
        }

        // POST: /Perguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titulo,QuestionarioId")] Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                db.Perguntas.Add(pergunta);
                db.SaveChanges();
                return RedirectToAction("Index", new { questionarioId = pergunta.QuestionarioId });
            }

            ViewBag.QuestionarioId = pergunta.QuestionarioId;
            return View(pergunta);
        }

        // GET: /Perguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pergunta pergunta = db.Perguntas.Find(id);
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            //ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", pergunta.QuestionarioId);
            return View(pergunta);
        }

        // POST: /Perguntas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titulo,QuestionarioId")] Pergunta pergunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pergunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { questionarioId = pergunta.QuestionarioId });
            }
            ViewBag.QuestionarioId = pergunta.QuestionarioId;
            return View(pergunta);
        }

        // GET: /Perguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pergunta pergunta = db.Perguntas.Find(id);
            if (pergunta == null)
            {
                return HttpNotFound();
            }
            return View(pergunta);
        }

        // POST: /Perguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pergunta pergunta = db.Perguntas.Find(id);
            db.Perguntas.Remove(pergunta);
            db.SaveChanges();
            return RedirectToAction("Index", new { questionarioId = pergunta.QuestionarioId });
        }

        //[HttpPost, ActionName("Filtro")]
        //public ActionResult Filtro(string titulo, int? questionarioId)
        //{
        //    var perguntas = db.Perguntas.Include(p => p.Questionario);

        //    if (!String.IsNullOrWhiteSpace(titulo))
        //        perguntas = perguntas.Where(w => w.Titulo.ToUpper() == titulo.ToUpper());

        //    if (questionarioId != null)
        //        perguntas = perguntas.Where(w => w.QuestionarioId == questionarioId);

        //    var filtro = perguntas.ToList();

        //    ViewBag.Questionarios = new SelectList(db.Questionarios, "Id", "Nome");

        //    return View("Index", filtro);            
        //}

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
