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
    public class ASCENSOSController : Controller
    {
        private Entities db = new Entities();

        // GET: ASCENSOS
        public ActionResult Index()
        {
            var aSCENSOS = db.ASCENSOS.Include(a => a.EMPLEADOS).Include(a => a.PUESTOS);
            return View(aSCENSOS.ToList());
        }

        // GET: ASCENSOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASCENSOS aSCENSOS = db.ASCENSOS.Find(id);
            if (aSCENSOS == null)
            {
                return HttpNotFound();
            }
            return View(aSCENSOS);
        }

        // GET: ASCENSOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO");
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
            return View();
        }

        // POST: ASCENSOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,AUTORIZACION")] ASCENSOS aSCENSOS)
        {
            if (ModelState.IsValid)
            {
                db.ASCENSOS.Add(aSCENSOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aSCENSOS.ID_EMPLEADO);
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", aSCENSOS.PUESTO_NVO);
            return View(aSCENSOS);
        }

        // GET: ASCENSOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASCENSOS aSCENSOS = db.ASCENSOS.Find(id);
            if (aSCENSOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aSCENSOS.ID_EMPLEADO);
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", aSCENSOS.PUESTO_NVO);
            return View(aSCENSOS);
        }

        // POST: ASCENSOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,AUTORIZACION")] ASCENSOS aSCENSOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aSCENSOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "ID_EMPLEADO", aSCENSOS.ID_EMPLEADO);
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", aSCENSOS.PUESTO_NVO);
            return View(aSCENSOS);
        }

        // GET: ASCENSOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASCENSOS aSCENSOS = db.ASCENSOS.Find(id);
            if (aSCENSOS == null)
            {
                return HttpNotFound();
            }
            return View(aSCENSOS);
        }

        // POST: ASCENSOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ASCENSOS aSCENSOS = db.ASCENSOS.Find(id);
            db.ASCENSOS.Remove(aSCENSOS);
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
