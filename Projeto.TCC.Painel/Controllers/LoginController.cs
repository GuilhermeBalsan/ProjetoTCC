using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.TCC.Painel.Models;
using System.Web.Security;

namespace Projeto.TCC.Painel.Controllers
{
    public class LoginController : Controller
    {
        private ProjetoTCCPainelContext db = new ProjetoTCCPainelContext();

        //
        // GET: /Login/
        public ActionResult Index(bool erro = false)
        {
            if (erro)
            {
                @ViewBag.Mensagem = "Usuário ou Senha inválidos";
                erro = false;
            }

            return View();
        }

        public ActionResult Login(Administrador model)
        {
            var administrador = db.Administradors.Where(w => w.Nome == model.Nome && w.Senha == model.Senha).ToList();

            if (administrador.Count() > 0)
            {
                FormsAuthentication.SetAuthCookie(model.Nome, false);
                return RedirectToAction("Index", "Resultados");
            }
            else
            {                
                return RedirectToAction("Index", new { erro = true });
            }           

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
	}
}