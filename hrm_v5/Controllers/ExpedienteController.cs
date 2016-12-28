using hrm_v5.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrm_v5.Controllers
{
    public class ExpedienteController : Controller
    {
        private Entities db = new Entities();
        private EMPLEADOS Empleado;

        // GET: VACACIONES/Create
        public ActionResult Create()
        {
            var EMP = db.EMPLEADOS;
            if (Empleado != null)
            {
                EMP.Add(Empleado);
                ViewBag.ID_EMPLEADO = new SelectList(EMP, "EMP_ID", "ID_EMPLEADO");
            }
            else 
            {
                ViewBag.ID_EMPLEADO = new SelectList(EMP, "EMP_ID", "ID_EMPLEADO");
            }
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
            var EMP = db.EMPLEADOS;
            if (Empleado != null)
            {
                EMP.Add(Empleado);
                ViewBag.ID_EMPLEADO = new SelectList(EMP, "EMP_ID", "ID_EMPLEADO");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(EMP, "EMP_ID", "ID_EMPLEADO");
            }
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            TempData["Vacaciones"] = "algo";
            return View();
        }

        public ActionResult Expediente(string searchString)
        {
            var EMP = from d in db.EMPLEADOS
                      select d;

            if (!String.IsNullOrEmpty(searchString))
            {
                EMP = EMP.Where(s => s.CEDULA.Contains(searchString));
                Empleado = db.EMPLEADOS.Find(EMP.First().EMP_ID);
            }

            return View("Expediente", EMP);
        }

    }
}