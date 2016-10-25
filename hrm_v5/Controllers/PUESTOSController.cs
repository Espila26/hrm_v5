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
        public ActionResult Index(string searchString)
        {
            var PUE = from e in db.PUESTOS
                      select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                PUE = PUE.Where(s => s.NOMBRE.Contains(searchString));
            }

            return View(PUE);
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
            ViewData["ID"] = CrearID();
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
                TempData["Success"] = "El puesto ha sido creada exitosamente";
                return RedirectToAction("Index");
            }

            ViewBag.DEPARTAMENTO = new SelectList(db.DEPARTAMENTOS, "ID_DEPARTAMENTO", "NOMBRE", pUESTOS.DEPARTAMENTO);
            return View(pUESTOS);
        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "Se debe de seleccionar al menos un puesto";
                return RedirectToAction("Index");
            }
            else
            {
                if (Request.Form["Detalles"] != null)
                {
                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Details", "PUESTOS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "Solamente es posible ver los detalles de un puesto a la vez";
                        return RedirectToAction("Index");
                    }
                }
                else if (Request.Form["Editar"] != null)
                {

                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Edit", "PUESTOS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "Solamente es posible editar un puesto a la vez";
                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
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

        public string CrearID()
        {
            int cont = 0;
            string dia = @DateTime.Now.Day.ToString();
            string mes = @DateTime.Now.Month.ToString();
            string año = DateTime.Now.Year.ToString();
            string fecha = dia + mes + año;
            if (db.PUESTOS.Count() == 0)
            {
                return cont + "-" + fecha;
            }
            else
            {
                while (cont != db.PUESTOS.Count())
                {
                    cont++;
                    cont.ToString();
                }
                return cont + "-" + fecha;
            }
        }
    }
}
