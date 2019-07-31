using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chemitech.Models;
using Chemitech.ViewModel;

namespace Chemitech.Controllers
{
    public class UsuarioQuimicoesController : Controller
    {
        private Contexto db = new Contexto();
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult ValidarEmail(string email)
        {
            UsuarioQuimico u = db.UsuarioQuimico.Where(t => t.Email == email).FirstOrDefault();
            if (u != null)
            {
                return Json("s");
            }
            else
            {
                return Json("n");
            }
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateInput(false)]
        //public JsonResult AlterarStatus(string id)
        //{
        //    UsuarioQuimico u = db.UsuarioQuimico.Find(Convert.ToInt32(id));
        //    if (u != null)
        //    {
        //        if (u.Status)
        //            u.Status = false;
        //        else
        //            u.Status = true;
        //        db.Entry(u).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return Json(u.Status ? "t" : "f");
        //    }
        //    else
        //    {
        //        return Json("n");
        //    }
        //}
        // GET: UsuarioQuimicoes
        public ActionResult Index()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            var usuarioQuimico = db.UsuarioEmpresa.Where(x=> x.EmpresaQuimico.EmpresaId==id);
            return View(usuarioQuimico.ToList());
        }

        // GET: UsuarioQuimicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioQuimico usuarioQuimico = db.UsuarioQuimico.Find(id);
            if (usuarioQuimico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioQuimico);
        }

        // GET: UsuarioQuimicoes/Create
        public ActionResult Create()
        {
            UsuarioEmpresaQuimica usuarioQuimico = new UsuarioEmpresaQuimica();
            var all = db.EmpresaQuimico.ToList();
            var checkBoxList = new List<CheckBoxListItem>();
            foreach (var emp in all)
            {
                checkBoxList.Add(new CheckBoxListItem()
                {
                    Id = emp.Id,
                    Display = emp.NomeEmpresa,
                    IdChecked = false
                });
            }
            usuarioQuimico.Empresas = checkBoxList;
            return View(usuarioQuimico);
        }

        // POST: UsuarioQuimicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Email,Empresas")] UsuarioEmpresaQuimica usuarioQuimico)
        {
            if (ModelState.IsValid)
            {
                UsuarioQuimico usu = new UsuarioQuimico();
                usu.Nome = usuarioQuimico.Nome;
                usu.Email = usuarioQuimico.Email;
                db.UsuarioQuimico.Add(usu);
                db.SaveChanges();

                UsuarioEmpresa usuemp = new UsuarioEmpresa();
                
                for (int c = 0; c < usuarioQuimico.Empresas.Count; c++)
                {
                    if (usuarioQuimico.Empresas[c].IdChecked)
                    {
                        usuemp.UsuarioQuimicoId = usu.Id;
                        usuemp.EmpresaQuimicoId = usuarioQuimico.Empresas[c].Id;
                        db.UsuarioEmpresa.Add(usuemp);
                        db.SaveChanges();
                    }
                }
                
                

                TempData["MSG"] = "success|Cadastro realizado";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";
            return View(usuarioQuimico);
        }

        // GET: UsuarioQuimicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioQuimico usuarioQuimico = db.UsuarioQuimico.Find(id);
            if (usuarioQuimico == null)
            {
                return HttpNotFound();
            }
            
            return View(usuarioQuimico);
        }

        // POST: UsuarioQuimicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Email,Empresas")] UsuarioQuimico usuarioQuimico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarioQuimico).State = EntityState.Modified;
                db.SaveChanges();
                TempData["MSG"] = "success|Edição realizado";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Edição não realizado";
            //ViewBag.EmpresaId = new SelectList(db.Empresa, "Id", "NomeEmpresa", usuarioQuimico.EmpresaId);
            return View(usuarioQuimico);
        }

        // GET: UsuarioQuimicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioQuimico usuarioQuimico = db.UsuarioQuimico.Find(id);
            if (usuarioQuimico == null)
            {
                return HttpNotFound();
            }
            return View(usuarioQuimico);
        }

        // POST: UsuarioQuimicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsuarioQuimico usuarioQuimico = db.UsuarioQuimico.Find(id);
            db.UsuarioQuimico.Remove(usuarioQuimico);
            db.SaveChanges();
            TempData["MSG"] = "success|Usuário deletado";

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
