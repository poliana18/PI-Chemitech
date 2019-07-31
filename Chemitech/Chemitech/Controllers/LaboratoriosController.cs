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
    public class LaboratoriosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Laboratorios
        public ActionResult Index()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            var laboratorio = db.Laboratorio.Where(x => x.EmpresaQuimico.EmpresaId == id).Include(l => l.EmpresaQuimico);
            return View(laboratorio.ToList());
        }

        // GET: Laboratorios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // GET: Laboratorios/Create
        public ActionResult Create()
        {
            ViewBag.EmpresaQuimicoId = new SelectList(db.EmpresaQuimico, "Id", "NomeEmpresa");
            return View();
        }

        // POST: Laboratorios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Localizacao,EmpresaQuimicoId")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Laboratorio.Add(laboratorio);
                db.SaveChanges();
                TempData["MSG"] = "success|Cadastro realizado";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";
            ViewBag.EmpresaQuimicoId = new SelectList(db.EmpresaQuimico, "Id", "NomeEmpresa", laboratorio.EmpresaQuimicoId);
            return View(laboratorio);
        }

        // GET: Laboratorios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmpresaQuimicoId = new SelectList(db.EmpresaQuimico, "Id", "NomeEmpresa", laboratorio.EmpresaQuimicoId);
            return View(laboratorio);
        }

        // POST: Laboratorios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Localizacao,EmpresaQuimicoId")] Laboratorio laboratorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(laboratorio).State = EntityState.Modified;
                db.SaveChanges();
                TempData["MSG"] = "success|Edição realizada";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Edição não realizada";
            ViewBag.EmpresaQuimicoId = new SelectList(db.EmpresaQuimico, "Id", "NomeEmpresa", laboratorio.EmpresaQuimicoId);
            return View(laboratorio);
        }

        // GET: Laboratorios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            if (laboratorio == null)
            {
                return HttpNotFound();
            }
            return View(laboratorio);
        }

        // POST: Laboratorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Laboratorio laboratorio = db.Laboratorio.Find(id);
            db.Laboratorio.Remove(laboratorio);
            db.SaveChanges();
            TempData["MSG"] = "success|Laboratório deletada";
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
