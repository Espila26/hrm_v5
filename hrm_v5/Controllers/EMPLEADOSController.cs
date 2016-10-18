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
    public class EMPLEADOSController : Controller
    {
        private Entities db = new Entities();

        // GET: EMPLEADOS
        public ActionResult Index(string searchString)
        {
            var EMP = from e in db.EMPLEADOS
                      select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                EMP = EMP.Where(s => s.NOMBRE.Contains(searchString) || s.APE1.Contains(searchString) || s.APE2.Contains(searchString) || s.CEDULA.Contains(searchString));
            }

            return View(EMP);
        }

        // GET: EMPLEADOS/Details/5
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

        // GET: EMPLEADOS/Create
        public ActionResult Create()
        {
            ViewData["ID"] = CrearID();
            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
            return View();
        }

        // POST: EMPLEADOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMP_ID,ID_EMPLEADO,CEDULA,NOMBRE,APE1,APE2,DIRECCION,DESCRIPCION,TEL_HABITACION,TEL_MOVIL,E_MAIL,PUESTO,SALARIO,ESTADO")] EMPLEADOS eMPLEADOS)
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

        // GET: EMPLEADOS/Edit/5
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

        // POST: EMPLEADOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMP_ID,ID_EMPLEADO,CEDULA,NOMBRE,APE1,APE2,DIRECCION,DESCRIPCION,TEL_HABITACION,TEL_MOVIL,E_MAIL,PUESTO,SALARIO,ESTADO")] EMPLEADOS eMPLEADOS)
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

        // GET: EMPLEADOS/Delete/5
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

        public ActionResult Expediente() {
            return View();
        }

        // POST: EMPLEADOS/Delete/5
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


        //Buscar un empleado en específico.

        public int CrearID(){
            string result;
            int a;
            int cont = 1;
            string varCont = cont.ToString();
            string dia = @DateTime.Now.Day.ToString();
            string mes = @DateTime.Now.Month.ToString();
            string año = DateTime.Now.Year.ToString();
            string fecha = dia + mes + año;
            if (db.EMPLEADOS.Count() == 0)
            {
                result = varCont + fecha;
                a = Int32.Parse(result);
                return a;
            }
            else
            {
                while (cont != db.EMPLEADOS.Count())
                {
                    cont++;
                }
                result = varCont + fecha;
                a = Int32.Parse(result);
                return a;
            }
        }
    }
}
