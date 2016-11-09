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
    public class EMPLEADOS1Controller : Controller
    {
        private Entities db = new Entities();

        // GET: EMPLEADOS1
        public ActionResult Index()
        {
            var eMPLEADOS = db.EMPLEADOS.Include(e => e.PUESTOS);
            return View(eMPLEADOS.ToList());
        }

        // GET: EMPLEADOS1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            if (eMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADOS);
        }

        // GET: EMPLEADOS1/Create
        public ActionResult Create()
        {
            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
            return View();
        }

        // POST: EMPLEADOS1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMP_ID,ID_EMPLEADO,CEDULA,NOMBRE,APE1,APE2,DIRECCION,DESCRIPCION,TEL_HABITACION,TEL_MOVIL,E_MAIL,PUESTO,SALARIO,ESTADO,FECHA_NAC")] EMPLEADOS eMPLEADOS)
        {
            if (ModelState.IsValid)
            {
                db.EMPLEADOS.Add(eMPLEADOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", eMPLEADOS.PUESTO);
            return View(eMPLEADOS);
        }

        // GET: EMPLEADOS1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            if (eMPLEADOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", eMPLEADOS.PUESTO);
            return View(eMPLEADOS);
        }

        // POST: EMPLEADOS1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMP_ID,ID_EMPLEADO,CEDULA,NOMBRE,APE1,APE2,DIRECCION,DESCRIPCION,TEL_HABITACION,TEL_MOVIL,E_MAIL,PUESTO,SALARIO,ESTADO,FECHA_NAC")] EMPLEADOS eMPLEADOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLEADOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", eMPLEADOS.PUESTO);
            return View(eMPLEADOS);
        }

        // GET: EMPLEADOS1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            if (eMPLEADOS == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADOS);
        }

        // POST: EMPLEADOS1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLEADOS eMPLEADOS = db.EMPLEADOS.Find(id);
            db.EMPLEADOS.Remove(eMPLEADOS);
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
