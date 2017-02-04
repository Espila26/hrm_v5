using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hrm_v5.Models;

namespace hrm_v5.Controllers
{
    public class AMONESTACIONESController : Controller
    {
        private Entities db = new Entities();

        // GET: AMONESTACIONES
        public ActionResult Index()
        {
            var aMONESTACIONES = db.AMONESTACIONES.Include(a => a.EMPLEADOS);
            return View(aMONESTACIONES.ToList());
        }

        // GET: AMONESTACIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMONESTACIONES aMONESTACIONES = db.AMONESTACIONES.Find(id);
            if (aMONESTACIONES == null)
            {
                return HttpNotFound();
            }
            return View(aMONESTACIONES);
        }

        // GET: AMONESTACIONES/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO");
            return View();
        }

        // POST: AMONESTACIONES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_AMONESTACION,ID_EMPLEADO,FECHA_INICIO,FECHA_FINAL,DESCRIPCION,GOCE_SALARIO,VERB_ESC,AUTORIZACION")] AMONESTACIONES aMONESTACIONES)
        {
            if (ModelState.IsValid)
            {
                db.AMONESTACIONES.Add(aMONESTACIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aMONESTACIONES.ID_EMPLEADO);
            return View(aMONESTACIONES);
        }

        // GET: AMONESTACIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMONESTACIONES aMONESTACIONES = db.AMONESTACIONES.Find(id);
            if (aMONESTACIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aMONESTACIONES.ID_EMPLEADO);
            return View(aMONESTACIONES);
        }

        // POST: AMONESTACIONES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_AMONESTACION,ID_EMPLEADO,FECHA_INICIO,FECHA_FINAL,DESCRIPCION,GOCE_SALARIO,VERB_ESC,AUTORIZACION")] AMONESTACIONES aMONESTACIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aMONESTACIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aMONESTACIONES.ID_EMPLEADO);
            return View(aMONESTACIONES);
        }

        // GET: AMONESTACIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AMONESTACIONES aMONESTACIONES = db.AMONESTACIONES.Find(id);
            if (aMONESTACIONES == null)
            {
                return HttpNotFound();
            }
            return View(aMONESTACIONES);
        }

        // POST: AMONESTACIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AMONESTACIONES aMONESTACIONES = db.AMONESTACIONES.Find(id);
            db.AMONESTACIONES.Remove(aMONESTACIONES);
            db.SaveChanges();
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
