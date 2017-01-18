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
    public class PERMISOSController : Controller
    {
        private Entities db = new Entities();

        // GET: PERMISOS
        public ActionResult Index()
        {
            var pERMISOS = db.PERMISOS.Include(p => p.EMPLEADOS);
            return View(pERMISOS.ToList());
        }

        // GET: PERMISOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            if (pERMISOS == null)
            {
                return HttpNotFound();
            }
            return View(pERMISOS);
        }

        // GET: PERMISOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA");
            return View();
        }

        // POST: PERMISOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                db.PERMISOS.Add(pERMISOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", pERMISOS.ID_EMPLEADO);
            return View(pERMISOS);
        }

        // GET: PERMISOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISOS pERMISOS = db.PERMISOS.Find(id);
            if (pERMISOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", pERMISOS.ID_EMPLEADO);
            return View(pERMISOS);
        }

        // POST: PERMISOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERMISOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADOS, "EMP_ID", "CEDULA", pERMISOS.ID_EMPLEADO);
            return View(pERMISOS);
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
