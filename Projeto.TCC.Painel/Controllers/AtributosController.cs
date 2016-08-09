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
    public class AtributosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Atributos/
        public ActionResult Index()
        {
            return View(db.Atributos.ToList());
        }

        // GET: /Atributos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atributo atributo = db.Atributos.Find(id);
            if (atributo == null)
            {
                return HttpNotFound();
            }
            return View(atributo);
        }

        // GET: /Atributos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Atributos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Titulo")] Atributo atributo)
        {
            if (ModelState.IsValid)
            {
                db.Atributos.Add(atributo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atributo);
        }

        // GET: /Atributos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atributo atributo = db.Atributos.Find(id);
            if (atributo == null)
            {
                return HttpNotFound();
            }
            return View(atributo);
        }

        // POST: /Atributos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Titulo")] Atributo atributo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atributo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atributo);
        }

        // GET: /Atributos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Atributo atributo = db.Atributos.Find(id);
            if (atributo == null)
            {
                return HttpNotFound();
            }
            return View(atributo);
        }

        // POST: /Atributos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Atributo atributo = db.Atributos.Find(id);
            db.Atributos.Remove(atributo);
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
