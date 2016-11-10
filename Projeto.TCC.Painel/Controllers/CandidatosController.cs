using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto.TCC.Painel.Models;
using System.IO;

namespace Projeto.TCC.Painel.Controllers
{
    public class CandidatosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Candidatos/
        public ActionResult Index()
        {
            var Candidatos = db.Candidatos.Include(u => u.Questionario);
            return View(Candidatos.ToList());
        }

        // GET: /Candidatos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato Candidato = db.Candidatos.Find(id);
            if (Candidato == null)
            {
                return HttpNotFound();
            }
            return View(Candidato);
        }

        // GET: /Candidatos/Create
        public ActionResult Create()
        {
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome");
            return View();
        }

        // POST: /Candidatos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Senha,Curriculum, QuestionarioId")] Candidato Candidato, HttpPostedFileBase file)
        {
           if (ModelState.IsValid && Request.Files.Count > 0 && file != null && file.ContentLength > 0)
            {
                var arquivo = Request.Files[0];
                
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(path);
                Candidato.Curriculum = "/Files/" + fileName;

                db.Candidatos.Add(Candidato);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", Candidato.QuestionarioId);
            return View(Candidato);
        }

        // GET: /Candidatos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato Candidato = db.Candidatos.Find(id);
            if (Candidato == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", Candidato.QuestionarioId);
            return View(Candidato);
        }

        // POST: /Candidatos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Senha,Curriculum,QuestionarioId")] Candidato Candidato, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && Request.Files.Count > 0 && file != null && file.ContentLength > 0)
            {
                var arquivo = Request.Files[0];

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(path);
                Candidato.Curriculum = "/Files/" + fileName;

                db.Entry(Candidato).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", Candidato.QuestionarioId);
            return View(Candidato);
        }

        // GET: /Candidatos/Delete/5
        public ActionResult Delete(int? id, bool erro = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidato Candidato = db.Candidatos.Find(id);
            if (Candidato == null)
            {
                return HttpNotFound();
            }
            if (erro)
            {
                ViewBag.Mensagem = "Não é possível apagar o Candidato pois existe resultado associado ao mesmo";
                erro = false;
            }
            return View(Candidato);
        }

        // POST: /Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidato Candidato = db.Candidatos.Find(id);
            db.Candidatos.Remove(Candidato);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return RedirectToAction("Delete", new { erro = true, id = id } );
            }
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
