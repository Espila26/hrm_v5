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
    public class SUSPENSIONESController : Controller
    {
        private Entities db = new Entities();

        // GET: SUSPENSIONES
        public ActionResult Index()
        {
            var sUSPENSIONES = db.SUSPENSIONES.Include(s => s.EMPLEADOS);
            return View(sUSPENSIONES.ToList());
        }

        // GET: SUSPENSIONES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUSPENSIONES sUSPENSIONES = db.SUSPENSIONES.Find(id);
            if (sUSPENSIONES == null)
            {
                return HttpNotFound();
            }
            return View(sUSPENSIONES);
        }

        // GET: SUSPENSIONES/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA");
            return View();
        }

        // POST: SUSPENSIONES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,DESCRIPCION,GOCE_SALARIO")] SUSPENSIONES sUSPENSIONES)
        {
            if (ModelState.IsValid)
            {
                db.SUSPENSIONES.Add(sUSPENSIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", sUSPENSIONES.ID_EMPLEADO);
            return View(sUSPENSIONES);
        }

        // GET: SUSPENSIONES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUSPENSIONES sUSPENSIONES = db.SUSPENSIONES.Find(id);
            if (sUSPENSIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", sUSPENSIONES.ID_EMPLEADO);
            return View(sUSPENSIONES);
        }

        // POST: SUSPENSIONES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,DESCRIPCION,GOCE_SALARIO")] SUSPENSIONES sUSPENSIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUSPENSIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", sUSPENSIONES.ID_EMPLEADO);
            return View(sUSPENSIONES);
        }

        // GET: SUSPENSIONES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUSPENSIONES sUSPENSIONES = db.SUSPENSIONES.Find(id);
            if (sUSPENSIONES == null)
            {
                return HttpNotFound();
            }
            return View(sUSPENSIONES);
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
