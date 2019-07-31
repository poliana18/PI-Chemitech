using Chemitech.Models;
using Chemitech.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Chemitech.Controllers
{
    public class EmpresaColetaController : Controller
    {
        private Contexto db = new Contexto();
        //GET: EmpresaColeta
        public ActionResult Index(int? id)
        {
            int ids = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            var e = db.EmpresaQuimico.Where(w => w.Empresa.Id == ids).Distinct().ToList();

            return View(e);
        }

        public ActionResult Delete(int id)
        {
            Bombona deletarBombona = db.Bombona.Find(id);

            if (deletarBombona == null)
            {
                return HttpNotFound();

            }

            db.Bombona.Remove(deletarBombona);
            db.SaveChanges();
            TempData["MSG"] = "success|Bombona coletada";
            return RedirectToAction("Index", "EmpresaColeta");
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(RecuperarSenha rec)
        {

            UsuarioColeta usu = db.UsuarioColeta.Where(t => t.Email == rec.Email).ToList().FirstOrDefault();
            if (usu != null)
            {
                string hash = Funcoes.HashTexto(DateTime.Now.ToString("yyyyMMddHHmmss"), "SHA512");
                hash = hash.Replace("\\", "").Replace("+", "").Replace("&", "").Replace("/", "");
                usu.RedefinirSenha = hash;
                usu.Datalimite = DateTime.Now.AddDays(1);
                db.Entry(usu).State = EntityState.Modified;
                db.SaveChanges();

                string assunto = "Alteraçao de senha";
                string mensagem = "Para alterar sua senha acesse o link abaixo: <br/>";
                mensagem += "<a href='http://localhost:56266/Homepage/RedefinirSenha/" + hash + "'>Clique aqui para alterar sua senha</a>";
                if (Funcoes.EnviarEmail(rec.Email, assunto, mensagem) != "error|Erro ao enviar e-mail")
                {
                    TempData["MSG"] = "success|Redefinição de senha enviada por e-mail";
                    return RedirectToAction("Index", "EmpresaColeta");
                }
                else
                {
                    ModelState.AddModelError("", "Erro ao enviar e-mail!");
                    return View();
                }
            }
            else

            {
                ModelState.AddModelError("", "Este E-mail não pertence a nenhuma conta!");
                return View();
            }

        }

        public ActionResult RedefinirSenha(string id)
        {
            UsuarioColeta usu = db.UsuarioColeta.Where(t => t.RedefinirSenha == id).ToList().FirstOrDefault();
            if (usu != null)
            {
                if (DateTime.Now <= usu.Datalimite)
                {
                    RedefinirSenha red = new RedefinirSenha();
                    red.Email = usu.Email;
                    ViewBag.Ocultar = "login";
                    return View(red);
                }
                else
                {
                    TempData["MSG"] = "warning|Data limite de alteração expirada";
                    return RedirectToAction("Index", "EmpresaColeta");
                }
            }
            else
            {
                TempData["MSG"] = "warning|Hash inválida";
                return RedirectToAction("Index", "EmpresaColeta");
            }

        }

        [HttpPost]
        public ActionResult RedefinirSenha(RedefinirSenha red, string id)
        {
            if (ModelState.IsValid)
            {
                UsuarioColeta usu = db.UsuarioColeta.Where(t => t.Email == red.Email).ToList().FirstOrDefault();
                if (usu != null)
                {
                    usu.Senha = Funcoes.HashTexto(red.Senha, "SHA512");
                    usu.RedefinirSenha = null;
                    usu.Datalimite = null;
                    db.Entry(usu).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["MSG"] = "success|Senha alterada com sucesso";
                    return RedirectToAction("Index", "EmpresaColeta");
                }
                else
                {
                    TempData["MSG"] = "danger|Erro ao alterar senha";
                    return RedirectToAction("Index", "EmpresaColeta");
                }


            }
            else
            {
                return View(red);
            }
        }



    }
}