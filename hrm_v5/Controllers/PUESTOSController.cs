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
    public class PUESTOSController : Controller
    {
        private Entities db = new Entities();

        // GET: PUESTOS
        public ActionResult Index()
        {
            var pUESTOS = db.PUESTOS.Include(p => p.DEPARTAMENTOS);
            return View(pUESTOS.ToList());
        }

        // GET: PUESTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTOS pUESTOS = db.PUESTOS.Find(id);
            if (pUESTOS == null)
            {
                return HttpNotFound();
            }
            return View(pUESTOS);
        }

        // GET: PUESTOS/Create
        public ActionResult Create()
        {
            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "ID_DEPARTAMENTO", "NOMBRE");
            return View();
        }

        // POST: PUESTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PTS_ID,ID_PUESTO,NOMBRE,DEPARTAMENTO,NIVEL_ACADEMICO,EXP_MIN,EXP_DESEADA,DESCRIPCION")] PUESTOS pUESTOS)
        {
            if (ModelState.IsValid)
            {
                db.PUESTOS.Add(pUESTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "ID_DEPARTAMENTO", "NOMBRE", pUESTOS.DEPARTAMENTO);
            return View(pUESTOS);
        }

        // GET: PUESTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTOS pUESTOS = db.PUESTOS.Find(id);
            if (pUESTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "ID_DEPARTAMENTO", "NOMBRE", pUESTOS.DEPARTAMENTO);
            return View(pUESTOS);
        }

        // POST: PUESTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PTS_ID,ID_PUESTO,NOMBRE,DEPARTAMENTO,NIVEL_ACADEMICO,EXP_MIN,EXP_DESEADA,DESCRIPCION")] PUESTOS pUESTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pUESTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "ID_DEPARTAMENTO", "NOMBRE", pUESTOS.DEPARTAMENTO);
            return View(pUESTOS);
        }

        // GET: PUESTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUESTOS pUESTOS = db.PUESTOS.Find(id);
            if (pUESTOS == null)
            {
                return HttpNotFound();
            }
            return View(pUESTOS);
        }

        // POST: PUESTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PUESTOS pUESTOS = db.PUESTOS.Find(id);
            db.PUESTOS.Remove(pUESTOS);
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
