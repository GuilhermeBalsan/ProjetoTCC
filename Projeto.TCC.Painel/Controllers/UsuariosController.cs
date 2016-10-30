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
    public class UsuariosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Usuarios/
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Questionario);
            return View(usuarios.ToList());
        }

        // GET: /Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: /Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome");
            return View();
        }

        // POST: /Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Senha,Curriculum, QuestionarioId")] Usuario usuario, HttpPostedFileBase file)
        {
           if (ModelState.IsValid && Request.Files.Count > 0 && file != null && file.ContentLength > 0)
            {
                var arquivo = Request.Files[0];
                
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(path);
                usuario.Curriculum = "/Files/" + fileName;

                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", usuario.QuestionarioId);
            return View(usuario);
        }

        // GET: /Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", usuario.QuestionarioId);
            return View(usuario);
        }

        // POST: /Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Senha,Curriculum,QuestionarioId")] Usuario usuario, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && Request.Files.Count > 0 && file != null && file.ContentLength > 0)
            {
                var arquivo = Request.Files[0];

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Files/"), fileName);
                file.SaveAs(path);
                usuario.Curriculum = "/Files/" + fileName;

                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionarioId = new SelectList(db.Questionarios, "Id", "Nome", usuario.QuestionarioId);
            return View(usuario);
        }

        // GET: /Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: /Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
