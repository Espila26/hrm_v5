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
        public ActionResult Index(string searchString)
        {
            var DEP = from d in db.DEPARTAMENTOS
                      select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                DEP = DEP.Where(s => s.NOMBRE.Contains(searchString));
                if (DEP.Count() == 0)
                    TempData["Error"] = "¡Los datos ingresados no pertenecen a ningún departamento asociado a la empresa!";
                return RedirectToAction("Index");
            }

            return View(DEP);
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
                TempData["Success"] = "¡El Departamento ha sido creado exitosamente!";
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
                TempData["Success"] = "¡La información del departamento ha sido editada exitosamente!";
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

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un departamento!";
                return RedirectToAction("Index");
            }
            else
            {
                if (Request.Form["Detalles"] != null)
                {
                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Details", "Departamentos", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible ver detalles de un departamento a la vez!";
                        return RedirectToAction("Index");
                    }
                }
                else if (Request.Form["Editar"] != null)
                {

                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Edit", "Departamentos", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible editar un departamento a la vez!";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
        }

    }
}
