﻿using hrm_v5.Models;
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
        /*********************************************************  Vacaciones  *********************************************************************/
        /*******************************************************************************************************************************************/
        // GET: VACACIONES
        public ActionResult Index()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var vACACIONES = db.VACACIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("Index", vACACIONES);
            //return View(vACACIONES.ToList());
        }

        // GET: VACACIONES/Create
        public ActionResult Create()
        {
            TempData.Keep("Empleado");
            ViewBagEmpleado();
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO");
            return View();
        }

        public ActionResult ValidateIDEmp()
        {
            if (TempData["Empleado"] == null)
            {
                TempData["Error"] = "¡Seleccione un empleado!";
                return RedirectToAction("Expediente", "Expediente");
            }
            else
            {
                //TempData["Success"] = "¡Seleccione un empleado!";
                return RedirectToAction("Index", "Expediente");
            }
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID_SOLICITUD,ID_EMPLEADO,INICIO,FINAL,CANT_DIAS,AUTORIZACION,FECHA_CREACION")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                vACACIONES.FECHA_CREACION = System.DateTime.Now;
                db.VACACIONES.Add(vACACIONES);
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            ViewBagEmpleado();
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            return View();
        }

        /*********************************************************  Suspenciones  *******************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateSusp(){
            TempData.Keep("Empleado");
            ViewBagEmpleado();
            return View();
        }

        [HttpPost]
        public ActionResult CreateSusp([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,DESCRIPCION,GOCE_SALARIO,AUTORIZACION")] SUSPENSIONES sUSPENSIONES){
            if (ModelState.IsValid)
            {
                sUSPENSIONES.FECHA_CREACION = System.DateTime.Now;
                db.SUSPENSIONES.Add(sUSPENSIONES);
                db.SaveChanges();
                return RedirectToAction("CreateSusp");
            }

            ViewBagEmpleado();
            return View(sUSPENSIONES);
        }

        public ActionResult IndexSusp()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var sUSPENSIONES = db.SUSPENSIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("IndexSusp", sUSPENSIONES);
        }

        public ActionResult ValidateIDEmpSup()
        {
            if (TempData["Empleado"] == null)
            {
                TempData["Error"] = "¡Seleccione un empleado!";
                return RedirectToAction("Expediente", "Expediente");
            }
            else
            {
                //TempData["Success"] = "¡Seleccione un empleado!";
                return RedirectToAction("IndexSusp", "Expediente");
            }
        }

        /**********************************************************   Permisos   ********************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreatePerm(){
            TempData.Keep("Empleado");
            ViewBagEmpleado();
            return View();
        }

        [HttpPost]
        public ActionResult CreatePerm([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS,AUTORIZACION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
                pERMISOS.FECHA_CREACION = System.DateTime.Now;
                db.PERMISOS.Add(pERMISOS);
                db.SaveChanges();
                return RedirectToAction("CreatePerm");
            }

            ViewBagEmpleado();
            return View(pERMISOS);
        }

        public ActionResult IndexPerm()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var pERMISOS = db.PERMISOS.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("IndexPerm", pERMISOS);
            //return View(vACACIONES.ToList());
        }

        public ActionResult ValidateIDEmpPerm()
        {
            if (TempData["Empleado"] == null)
            {
                TempData["Error"] = "¡Seleccione un empleado!";
                return RedirectToAction("Expediente", "Expediente");
            }
            else
            {
                //TempData["Success"] = "¡Seleccione un empleado!";
                return RedirectToAction("IndexPerm", "Expediente");
            }
        }

        /**********************************************************   Ascensos   ********************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateAsc(){
            ViewBagEmpleado();
            GetPuestoAnt();
            TempData.Keep("Empleado");
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "NOMBRE");
            return View();
        }

        [HttpPost]
        public ActionResult CreateAsc([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,AUTORIZACION")] ASCENSOS aSCENSOS){
            TempData.Keep("Empleado");
            if (ModelState.IsValid)
            {
                aSCENSOS.FECHA_CREACION = System.DateTime.Now;
                db.ASCENSOS.Add(aSCENSOS);
                EMPLEADOS temp = (EMPLEADOS)TempData["Empleado"];
                var Emp = db.EMPLEADOS.Find(temp.EMP_ID);
                Emp.PUESTO = aSCENSOS.PUESTO_NVO;
                db.SaveChanges();
                return RedirectToAction("CreateAsc");
            }
            GetPuestoAnt();
            ViewBagEmpleado();
            ViewBag.PUESTO_NVO = new SelectList(db.PUESTOS, "PTS_ID", "NOMBRE", aSCENSOS.PUESTO_NVO);
            return View(aSCENSOS);
        }

        public ActionResult IndexAsc()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var aSCENSOS = db.ASCENSOS.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("IndexAsc", aSCENSOS);
        }

        public ActionResult ValidateIDEmpAsc()
        {
            if (TempData["Empleado"] == null)
            {
                TempData["Error"] = "¡Seleccione un empleado!";
                return RedirectToAction("Expediente", "Expediente");
            }
            else
            {
                //TempData["Success"] = "¡Seleccione un empleado!";
                return RedirectToAction("IndexAsc", "Expediente");
            }
        }

        public void GetPuestoAnt(){
            EMPLEADOS temp = (EMPLEADOS)TempData["Empleado"];
            ViewData["PUESTO_ANT"] = temp.PUESTOS.NOMBRE;
        }

        /*******************************************************  Amonestaciones   ******************************************************************/
        /*******************************************************************************************************************************************/

        public ActionResult CreateAmon(){
            TempData.Keep("Empleado");
            ViewBagEmpleado();
            return View();
        }

        [HttpPost]
        public ActionResult CreateAmon([Bind(Include = "ID_AMONESTACION,ID_EMPLEADO,FECHA_INICIO,FECHA_FINAL,DESCRIPCION,GOCE_SALARIO,VERB_ESC,AUTORIZACION")] AMONESTACIONES aMONESTACIONES)
        {
            if (ModelState.IsValid)
            {
                aMONESTACIONES.FECHA_CREACION = System.DateTime.Now;
                db.AMONESTACIONES.Add(aMONESTACIONES);
                db.SaveChanges();
                return RedirectToAction("CreateAmon");
            }

            ViewBagEmpleado();
            return View(aMONESTACIONES);
        }

        public ActionResult IndexAmon()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var aMONESTACIONES = db.AMONESTACIONES.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("IndexAmon", aMONESTACIONES);
        }

        public ActionResult ValidateIDEmpAmon()
        {
            if (TempData["Empleado"] == null)
            {
                TempData["Error"] = "¡Seleccione un empleado!";
                return RedirectToAction("Expediente", "Expediente");
            }
            else
            {
                //TempData["Success"] = "¡Seleccione un empleado!";
                return RedirectToAction("IndexAmon", "Expediente");
            }
        }

        /*******************************************************  Datos Personales  *****************************************************************/
        /*******************************************************************************************************************************************/
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
                if (childChkbox.Count() == 1)
                {
                    TempData["Empleado"] = db.EMPLEADOS.Find(Int32.Parse(childChkbox.First()));
                    TempData["Success"] = "¡Se ha seleccionado empleado!";
                }
                else
                {
                    TempData["Error"] = "¡Solamente es posible ver los detalles de un empleado a la vez!";
                }
            }
            return RedirectToAction("Expediente");
        }

        public void ViewBagEmpleado()
        {
            List<EMPLEADOS> Empleado = new List<EMPLEADOS>();
            if (TempData["Empleado"] != null)
            {
                Empleado.Add((EMPLEADOS)TempData["Empleado"]);
                CalcularDiasDisponibles(Empleado.First());
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "NOMBRE");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "NOMBRE");
            }
        }

        public void CalcularDiasDisponibles(EMPLEADOS empleado)
        {
            int annos = DateTime.Now.Year - empleado.FECHA_CONTR.Year;
            int meses = DateTime.Now.Month - empleado.FECHA_CONTR.Month;
            int resMesIncomp = 0;
            if(DateTime.Now.Day < empleado.FECHA_CONTR.Day)
            {
                resMesIncomp = 1;
            }
            int diasDisponibles = (annos * 12) + meses - empleado.DIAS_VAC_UTILIZAD - resMesIncomp;
            ViewBag.DiasDisponibles = diasDisponibles;
        }

    }
}