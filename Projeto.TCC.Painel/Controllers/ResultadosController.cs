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
    public class ResultadosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Resultados/
        //public ActionResult Index()
        //{
        //    ViewBag.Questionarios = new SelectList(db.Questionarios, "Id", "Nome");
        //    var resultados = db.Resultados.Include(r => r.Atributo).Include(r => r.Questionario);
        //    return View(resultados.ToList());
        //}

        //[HttpPost, ActionName("Filtro")]
        //public ActionResult Filtro(string nome, int? questionarioId)
        //{
        //    var resultados = db.Resultados.Include(r => r.Atributo).Include(r => r.Questionario);

        //    if (!String.IsNullOrWhiteSpace(nome))
        //        resultados = resultados.Where(w => w.Nome.ToUpper() == nome.ToUpper());

        //    if (questionarioId != null)
        //        resultados = resultados.Where(w => w.QuestionarioId == questionarioId);

        //    var filtro = resultados.ToList();

        //    ViewBag.Questionarios = new SelectList(db.Questionarios, "Id", "Nome");

        //    return View("Index", filtro);
        //}

        
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
