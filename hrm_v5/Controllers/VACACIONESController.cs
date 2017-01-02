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
    public class VACACIONESController : Controller
    {
        private Entities db = new Entities();

        // GET: VACACIONES
        public ActionResult Index()
        {
            var vACACIONES = db.VACACIONES.Include(v => v.EMPLEADOS);
            return View(vACACIONES.ToList());
        }

        // GET: VACACIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONES);
        }

        // GET: VACACIONES/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO");
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
            return View();
        }

        // POST: VACACIONES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "ID_SOLICITUD,ID_EMPLEADO,INICIO,FINAL,CANT_DIAS,AUTORIZACION")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                db.VACACIONES.Add(vACACIONES);
                db.SaveChanges();
                return RedirectToAction("Expediente", "EMPLEADOS");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", vACACIONES.ID_EMPLEADO);
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            TempData["Vacaciones"] = "algo";
            return View();
        }

        // GET: VACACIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", vACACIONES.ID_EMPLEADO);
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            return View(vACACIONES);
        }

        // POST: VACACIONES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SOLICITUD,ID_EMPLEADO,INICIO,FINAL,CANT_DIAS,AUTORIZACION")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vACACIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", vACACIONES.ID_EMPLEADO);
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            return View(vACACIONES);
        }

        // GET: VACACIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONES);
        }

        // POST: VACACIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            db.VACACIONES.Remove(vACACIONES);
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
