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
    public class RelatoriosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Relatorios/
        public ActionResult Index(int PerfilId, string mensagem = "")
        {
            var relatorios = db.Relatorios.Include(r => r.Perfil).Where(w => w.PerfilId == PerfilId);

            ViewBag.PerfilId = PerfilId;
            ViewBag.Perfil = db.Perfils.Where(w => w.Id == PerfilId).Select(s => s.Titulo).FirstOrDefault();
            ViewBag.Mensagem = mensagem;

            return View(relatorios.ToList());
        }

        // GET: /Relatorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Relatorio relatorio = db.Relatorios.Find(id);

            if (relatorio == null)
            {
                return HttpNotFound();
            }

            var descricaoFormatada = WebUtility.HtmlDecode(relatorio.Mensagem);
            ViewBag.DescricaoHTML = descricaoFormatada;

            return View(relatorio);
        }

        // GET: /Relatorios/Create
        public ActionResult Create(int PerfilId)
        {
            int relatoriosCount = db.Relatorios.Where(w => w.Perfil.Id == PerfilId).Count();

            if (relatoriosCount > 0)
            {
                string mensagem = "Já existe um relatório cadastrado para esse Perfil";
                return RedirectToAction("Index", new { PerfilId = PerfilId, mensagem = mensagem });
            }

            ViewBag.PerfilId = PerfilId;
            return View();
        }

        // POST: /Relatorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Mensagem,PerfilId")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                int count = db.Relatorios.Where(w => w.PerfilId == relatorio.PerfilId).Count();

                if (count > 0)
                    return RedirectToAction("Index", new { PerfilId = relatorio.PerfilId });

                relatorio.Mensagem = relatorio.Mensagem.Replace("<img", "<img class=\"img-responsive\"");
                relatorio.Mensagem = WebUtility.HtmlEncode(relatorio.Mensagem);
                db.Relatorios.Add(relatorio);
                db.SaveChanges();
                return RedirectToAction("Index", new { PerfilId = relatorio.PerfilId });
            }

            ViewBag.PerfilId = relatorio.PerfilId;
            return View(relatorio);
        }

        // GET: /Relatorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = db.Relatorios.Find(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }

            var mensagemFormatada = WebUtility.HtmlDecode(relatorio.Mensagem);

            ViewBag.MensagemHTML = mensagemFormatada;

            //ViewBag.PerfilId = new SelectList(db.Perfils, "Id", "Titulo", relatorio.PerfilId);

            return View(relatorio);
        }

        // POST: /Relatorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Mensagem,PerfilId")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                relatorio.Mensagem = relatorio.Mensagem.Replace("<img", "<img class=\"img-responsive\"");
                relatorio.Mensagem = WebUtility.HtmlEncode(relatorio.Mensagem);
                db.Entry(relatorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { PerfilId = relatorio.PerfilId });
            }
            ViewBag.PerfilId = relatorio.PerfilId;
            return View(relatorio);
        }

        // GET: /Relatorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relatorio relatorio = db.Relatorios.Find(id);
            if (relatorio == null)
            {
                return HttpNotFound();
            }

            var descricaoFormatada = WebUtility.HtmlDecode(relatorio.Mensagem);
            ViewBag.DescricaoHTML = descricaoFormatada;

            return View(relatorio);
        }

        // POST: /Relatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Relatorio relatorio = db.Relatorios.Find(id);
            db.Relatorios.Remove(relatorio);
            db.SaveChanges();
            return RedirectToAction("Index", new { PerfilId = relatorio.PerfilId });
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
