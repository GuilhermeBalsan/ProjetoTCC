﻿using System;
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
            var query = db.Resultados.Select(s => new { s.QuestionarioId, s.Usuario.Questionario.Nome }).Distinct().ToList();
            
            List<Questionario> questionarios = new List<Questionario>();

            foreach(var q in query)
            {               

                Questionario questionario = new Questionario { Nome = q.Nome, Id = q.QuestionarioId };

                questionarios.Add(questionario);
            }

            return View(questionarios);
        }

        public ActionResult Candidatos(int id)
        {
            AdoNet adoNet = new AdoNet();
            DataTable dt = adoNet.ExecProcedure(id);

            return View(dt);
        }

        // GET: /Resultados/Details/5
        public ActionResult Details(int UsuarioId, int QuestionarioId)
        {
            if (UsuarioId == null && QuestionarioId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Resultado resultado = db.Resultados.Include(r => r.Usuario).Include(r => r.DetalhesResultado).Where(w => w.Id == id).FirstOrDefault();
            Resultado resultado = db.Resultados.Include(r => r.Usuario).Include(r => r.DetalhesResultado).Where(w => w.UsuarioId == UsuarioId && w.QuestionarioId == QuestionarioId).FirstOrDefault();
            if (resultado == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionarioId = QuestionarioId;
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
