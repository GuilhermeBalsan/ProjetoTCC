using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projeto.TCC.Painel.Models;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.Helpers;
//using System.Web.UI.DataVisualization.Charting;
//using System.Drawing;


namespace Projeto.TCC.Painel.Controllers
{
    public class ResultadosController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        // GET: /Resultados/
        public ActionResult Index()
        {
            var query = db.Resultados.Select(s => new { s.QuestionarioId, s.Candidato.Questionario.Nome }).Distinct().ToList();
            
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
            DataTable dt = adoNet.ExecProcedure(id, "RelatorioQuestionario");

            ViewBag.QuestionarioId = id;

            return View(dt);
        }

        public ActionResult Grafico(int questionarioId)
        {
            AdoNet adoNet = new AdoNet();
            DataTable dt = adoNet.ExecProcedure(questionarioId, "RelatorioQuestionarioGrafico");

            var xValues = new List<String>();            
            var yValue = new List<Decimal>();

            foreach (DataRow row in dt.Rows)
            {

                xValues.Add(row[0].ToString().Replace(",", "."));                
                yValue.Add(Convert.ToDecimal(row[2].ToString().Replace(",", ".")));
            }


            var chart = new Chart(600, 300);
            chart.AddSeries(chartType: "Pie", xValue: xValues, yValues: yValue);
            chart.AddTitle("Representação");            
            chart.Write("png");          

            return null;
        }

        // GET: /Resultados/Details/5
        public ActionResult Details(int CandidatoId, int QuestionarioId)
        {
            //if (CandidatoId == null && QuestionarioId == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Resultado resultado = db.Resultados.Include(r => r.Candidato).Include(r => r.DetalhesResultado).Where(w => w.Id == id).FirstOrDefault();
            Resultado resultado = db.Resultados.Include(r => r.Candidato).Include(r => r.DetalhesResultado).Where(w => w.CandidatoId == CandidatoId && w.QuestionarioId == QuestionarioId).FirstOrDefault();
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
