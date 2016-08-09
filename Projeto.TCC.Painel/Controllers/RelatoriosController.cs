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
        public ActionResult Index(int atributoId, string mensagem = "")
        {
            var relatorios = db.Relatorios.Include(r => r.Atributo).Where(w => w.AtributoId == atributoId);

            ViewBag.AtributoId = atributoId;
            ViewBag.Atributo = db.Atributos.Where(w => w.Id == atributoId).Select(s => s.Titulo).FirstOrDefault();
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
        public ActionResult Create(int atributoId)
        {
            int relatoriosCount = db.Relatorios.Where(w => w.Atributo.Id == atributoId).Count();

            if (relatoriosCount > 0)
            {
                string mensagem = "Já existe um relatório cadastrado para esse atributo";
                return RedirectToAction("Index", new { atributoId = atributoId, mensagem = mensagem });
            }

            ViewBag.AtributoId = atributoId;
            return View();
        }

        // POST: /Relatorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Mensagem,AtributoId")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                int count = db.Relatorios.Where(w => w.AtributoId == relatorio.AtributoId).Count();

                if (count > 0)
                    return RedirectToAction("Index", new { atributoId = relatorio.AtributoId });

                relatorio.Mensagem = relatorio.Mensagem.Replace("<img", "<img class=\"img-responsive\"");
                relatorio.Mensagem = WebUtility.HtmlEncode(relatorio.Mensagem);
                db.Relatorios.Add(relatorio);
                db.SaveChanges();
                return RedirectToAction("Index", new { atributoId = relatorio.AtributoId });
            }

            ViewBag.AtributoId = relatorio.AtributoId;
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

            //ViewBag.AtributoId = new SelectList(db.Atributos, "Id", "Titulo", relatorio.AtributoId);

            return View(relatorio);
        }

        // POST: /Relatorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Mensagem,AtributoId")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                relatorio.Mensagem = relatorio.Mensagem.Replace("<img", "<img class=\"img-responsive\"");
                relatorio.Mensagem = WebUtility.HtmlEncode(relatorio.Mensagem);
                db.Entry(relatorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { atributoId = relatorio.AtributoId });
            }
            ViewBag.AtributoId = relatorio.AtributoId;
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
            return RedirectToAction("Index", new { atributoId = relatorio.AtributoId });
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
