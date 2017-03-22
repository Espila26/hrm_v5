using hrm_v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hrm_v5.Controllers
{
    public class ReportesController : Controller
    {
        private Entities db = new Entities();
        // GET: Reportes
        public ActionResult GenerarReporte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Generar(string datoEmpleado, string Estado, string desde, string hasta, string accion)
        {
            if (accion.Equals("Vacaciones"))
            {
      
                var vACACIONES = db.VACACIONES.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.FECHA_CREACION >= DateTime.Parse(desde) && s.FECHA_CREACION <= DateTime.Parse(hasta)) && (s.EMPLEADOS.ESTADO.Contains(Estado)));
                return View("ReporteVacaciones", vACACIONES);
            }
            TempData["Error"] = "dato: " + datoEmpleado + " estado: " + Estado + " desde: " + desde + " hasta: " + hasta + " Accion: " + accion;
            return RedirectToAction("GenerarReporte");
        }
    }
}