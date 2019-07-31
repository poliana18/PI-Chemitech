using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chemitech.Models;

namespace Chemitech.Controllers
{
    public class EmpresasController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Empresas
        public ActionResult Index()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            var empresa = db.EmpresaQuimico.Where(w => w.Empresa.Id == id).Include(e => e.RamoAtividade);
            return View(empresa.ToList());
        }

        // GET: Empresas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaQuimico empresa = db.EmpresaQuimico.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // GET: Empresas/Create
        public ActionResult Create()
        {
           ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeEmpresa,Cnpj,Endereco,Cidade,Telefone,RamoAtividadeId")] EmpresaQuimico empresa)
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            empresa.EmpresaId = id;

            if (ModelState.IsValid)
            {
                
                db.EmpresaQuimico.Add(empresa);
                db.SaveChanges();

                
                TempData["MSG"] = "success|Cadastro realizado";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";
            ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");
            
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaQuimico empresa = db.EmpresaQuimico.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeEmpresa,Cnpj,Endereco,Cidade,Telefone,RamoAtividadeId")] EmpresaQuimico empresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empresa).State = EntityState.Modified;
                db.SaveChanges();
                TempData["MSG"] = "success|Empresa editada com sucesso";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Empresa não editada";
            ViewBag.RamoAtividadeId = new SelectList(db.RamoAtividade, "Id", "NomeRamo");
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmpresaQuimico empresa = db.EmpresaQuimico.Find(id);
            if (empresa == null)
            {
                return HttpNotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpresaQuimico empresa = db.EmpresaQuimico.Find(id);
            db.EmpresaQuimico.Remove(empresa);
            db.SaveChanges();
            TempData["MSG"] = "success|Empresa deletada";
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
