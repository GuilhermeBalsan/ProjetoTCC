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
    public class PerfilsController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Perfils/
        public ActionResult Index()
        {
            return View(db.Perfils.ToList());
        }

        // GET: /Perfils/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil Perfil = db.Perfils.Find(id);
            if (Perfil == null)
            {
                return HttpNotFound();
            }
            return View(Perfil);
        }

        // GET: /Perfils/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Perfils/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titulo")] Perfil Perfil)
        {
            if (ModelState.IsValid)
            {
                db.Perfils.Add(Perfil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Perfil);
        }

        // GET: /Perfils/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil Perfil = db.Perfils.Find(id);
            if (Perfil == null)
            {
                return HttpNotFound();
            }
            return View(Perfil);
        }

        // POST: /Perfils/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titulo")] Perfil Perfil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Perfil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Perfil);
        }

        // GET: /Perfils/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfil Perfil = db.Perfils.Find(id);
            if (Perfil == null)
            {
                return HttpNotFound();
            }
            return View(Perfil);
        }

        // POST: /Perfils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perfil Perfil = db.Perfils.Find(id);
            db.Perfils.Remove(Perfil);
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
