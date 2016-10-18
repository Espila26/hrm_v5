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
    public class DEPARTAMENTOSController : Controller
    {
        private Entities db = new Entities();

        // GET: DEPARTAMENTOS
        public ActionResult Index()
        {
            var dEPARTAMENTOS = db.DEPARTAMENTOS.Include(d => d.EMPRESAS);
            return View(dEPARTAMENTOS.ToList());
        }

        // GET: DEPARTAMENTOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Create
        public ActionResult Create()
        {
            ViewBag.EMPRESA = new SelectList(db.EMPRESAS, "ID_EMPRESA", "NOMBRE");
            return View();
        }

        // POST: DEPARTAMENTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DEPARTAMENTO,NOMBRE,DESCRIPCION,EMPRESA")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTAMENTOS.Add(dEPARTAMENTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMPRESA = new SelectList(db.EMPRESAS, "ID_EMPRESA", "NOMBRE", dEPARTAMENTOS.EMPRESA);
            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMPRESA = new SelectList(db.EMPRESAS, "ID_EMPRESA", "NOMBRE", dEPARTAMENTOS.EMPRESA);
            return View(dEPARTAMENTOS);
        }

        // POST: DEPARTAMENTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DEPARTAMENTO,NOMBRE,DESCRIPCION,EMPRESA")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEPARTAMENTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMPRESA = new SelectList(db.EMPRESAS, "ID_EMPRESA", "NOMBRE", dEPARTAMENTOS.EMPRESA);
            return View(dEPARTAMENTOS);
        }

        // GET: DEPARTAMENTOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            if (dEPARTAMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dEPARTAMENTOS);
        }

        // POST: DEPARTAMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DEPARTAMENTOS dEPARTAMENTOS = db.DEPARTAMENTOS.Find(id);
            db.DEPARTAMENTOS.Remove(dEPARTAMENTOS);
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
