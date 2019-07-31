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
    public class BombonasController : Controller
    {
        private Contexto db = new Contexto();
        
        // GET: Bombonas
        public ActionResult Index()
        {
            int id = Convert.ToInt32(User.Identity.Name.Split('|')[2]);
            var bombona = db.Bombona.Include(b => b.Laboratorio.EmpresaQuimico).Where(x => x.Laboratorio.EmpresaQuimico.EmpresaId== id);
            return View(bombona.ToList());
        }

        // GET: Bombonas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bombona bombona = db.Bombona.Find(id);
            if (bombona == null)
            {
                return HttpNotFound();
            }
            return View(bombona);
        }

        // GET: Bombonas/Create
        public ActionResult Create()
        {
            ViewBag.LaboratorioId = new SelectList(db.Laboratorio, "Id", "Nome");
            return View();
        }

        // POST: Bombonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Capacidade,DataInstalacao,LaboratorioId")] Bombona bombona)
        {
            if (ModelState.IsValid)
            {
                bombona.DataInstalacao = DateTime.Now;
                db.Bombona.Add(bombona);
                db.SaveChanges();
                TempData["MSG"] = "success|Cadastro realizado";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Cadastro não realizado";
            ViewBag.LaboratorioId = new SelectList(db.Laboratorio, "Id", "Nome", bombona.LaboratorioId);
            return View(bombona);
        }

        // GET: Bombonas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bombona bombona = db.Bombona.Find(id);
            if (bombona == null)
            {
                return HttpNotFound();
            }
            ViewBag.LaboratorioId = new SelectList(db.Laboratorio, "Id", "Nome", bombona.LaboratorioId);
            return View(bombona);
        }

        // POST: Bombonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Capacidade,DataInstalacao,LaboratorioId")] Bombona bombona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bombona).State = EntityState.Modified;
                db.SaveChanges();
                TempData["MSG"] = "success|Edição realizada";
                return RedirectToAction("Index");
            }
            TempData["MSG"] = "Warning|Edição não realizada";
            ViewBag.LaboratorioId = new SelectList(db.Laboratorio, "Id", "Nome", bombona.LaboratorioId);
            return View(bombona);
        }

        // GET: Bombonas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bombona bombona = db.Bombona.Find(id);
            if (bombona == null)
            {
                return HttpNotFound();
            }
            return View(bombona);
        }

        // POST: Bombonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bombona bombona = db.Bombona.Find(id);
            db.Bombona.Remove(bombona);
            db.SaveChanges();
            TempData["MSG"] = "success|Bombona deletada";
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
