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
        public ActionResult Index()
        {
            var resultados = db.Resultados.Include(r => r.Usuario).Include(r => r.DetalhesResultado);
            return View(resultados.ToList());
        }

        // GET: /Resultados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultado resultado = db.Resultados.Include(r => r.Usuario).Include(r => r.DetalhesResultado).Where(w => w.Id == id).FirstOrDefault();
            if (resultado == null)
            {
                return HttpNotFound();
            }
            return View(resultado);
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
