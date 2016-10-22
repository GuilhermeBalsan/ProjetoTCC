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
    public class PesosQuestionariosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /PesosQuestionarios/
        public ActionResult Index()
        {
            var pesoquestionarios = db.PesoQuestionarios.Include(p => p.Atributos).Include(p => p.Questionarios);
            return View(pesoquestionarios.ToList());
        }

        // GET: /PesosQuestionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PesoQuestionario pesoquestionario = db.PesoQuestionarios.Find(id);
            if (pesoquestionario == null)
            {
                return HttpNotFound();
            }
            return View(pesoquestionario);
        }

        // GET: /PesosQuestionarios/Create
        public ActionResult Create()
        {
            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo");
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome");
            return View();
        }

        // POST: /PesosQuestionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,QuestionarioId,AtributoId,Peso")] PesoQuestionario pesoquestionario)
        {
            if (ModelState.IsValid)
            {
                db.PesoQuestionarios.Add(pesoquestionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", pesoquestionario.AtributoId);
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", pesoquestionario.QuestionarioId);
            return View(pesoquestionario);
        }

        // GET: /PesosQuestionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PesoQuestionario pesoquestionario = db.PesoQuestionarios.Find(id);
            if (pesoquestionario == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", pesoquestionario.AtributoId);
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", pesoquestionario.QuestionarioId);
            return View(pesoquestionario);
        }

        // POST: /PesosQuestionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,QuestionarioId,AtributoId,Peso")] PesoQuestionario pesoquestionario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pesoquestionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", pesoquestionario.AtributoId);
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", pesoquestionario.QuestionarioId);
            return View(pesoquestionario);
        }

        // GET: /PesosQuestionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PesoQuestionario pesoquestionario = db.PesoQuestionarios.Find(id);
            if (pesoquestionario == null)
            {
                return HttpNotFound();
            }
            return View(pesoquestionario);
        }

        // POST: /PesosQuestionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PesoQuestionario pesoquestionario = db.PesoQuestionarios.Find(id);
            db.PesoQuestionarios.Remove(pesoquestionario);
            db.SaveChanges();
            return RedirectToAction("Index");
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
