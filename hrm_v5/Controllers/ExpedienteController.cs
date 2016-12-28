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

        // GET: VACACIONES/Create
        public ActionResult Create()
        {
            List<object> Empleado = new List<Object>();
            if (TempData["Empleado"] != null)
            {
                Empleado.Add(TempData["Empleado"]);
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
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
            List<object> Empleado = new List<Object>();
            if (TempData["Empleado"] != null)
            {
                Empleado.Add(TempData["Empleado"]);
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
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
            }

            return View("Expediente", EMP);
        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un empleado!";
            }
            else
            {
                if (Request.Form["Seleccionar"] != null)
                {
                    if (childChkbox.Count() == 1)
                    {
                        TempData["Empleado"] = db.EMPLEADOS.Find(Int32.Parse(childChkbox.First()));
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible ver los detalles de un empleado a la vez!";
                    }
                }
            }
            return RedirectToAction("Expediente");
        }

    }
}