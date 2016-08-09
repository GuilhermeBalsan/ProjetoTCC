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
    public class QuestionariosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Questionarios/
        
        public ActionResult Index()
        {
            return View(db.Questionarios.ToList());
        }

        // GET: /Questionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }

            var descricaoFormatada = WebUtility.HtmlDecode(questionario.Descricao);
            ViewBag.DescricaoHTML = descricaoFormatada;

            return View(questionario);
        }

        // GET: /Questionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Questionarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Descricao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                questionario.Descricao = questionario.Descricao.Replace("<img", "<img class=\"img-responsive\"");
                questionario.Descricao = WebUtility.HtmlEncode(questionario.Descricao);
                db.Questionarios.Add(questionario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questionario);
        }

        // GET: /Questionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }

            var descricaoFormatada = WebUtility.HtmlDecode(questionario.Descricao);            

            ViewBag.DescricaoHTML = descricaoFormatada;

            return View(questionario);
        }

        // POST: /Questionarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Descricao")] Questionario questionario)
        {
            if (ModelState.IsValid)
            {
                questionario.Descricao = questionario.Descricao.Replace("<img", "<img class=\"img-responsive\"");
                questionario.Descricao = WebUtility.HtmlEncode(questionario.Descricao);
                db.Entry(questionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questionario);
        }

        // GET: /Questionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Questionario questionario = db.Questionarios.Find(id);
            if (questionario == null)
            {
                return HttpNotFound();
            }
            return View(questionario);
        }

        // POST: /Questionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Questionario questionario = db.Questionarios.Find(id);
            db.Questionarios.Remove(questionario);
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
