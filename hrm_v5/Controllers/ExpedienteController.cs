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
                return RedirectToAction("Create");
            }
            ViewBagEmpleado();
            ViewBag.AUTORIZACION = new SelectList(db.PUESTOS, "PTS_ID", "ID_PUESTO", vACACIONES.AUTORIZACION);
            return View();
        }

        /*********************************************************  Suspenciones  *******************************************************************/
        /*******************************************************************************************************************************************/

        [HttpPost]
        public ActionResult CreateSusp([Bind(Include = "ID_SUSPENSION,ID_EMPLEADO,INICIO,FINAL,DESCRIPCION,GOCE_SALARIO,AUTORIZACION")] SUSPENSIONES sUSPENSIONES)
        {
            if (ModelState.IsValid)
            {
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
            return View("sUSPENSIONES", sUSPENSIONES);
            //return View(vACACIONES.ToList());
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

        [HttpPost]
        public ActionResult CreatePerm([Bind(Include = "ID_PERMISO,ID_EMPLEADO,INICIO,FINAL,GOCE_SALARIO,CANT_HORAS,CANT_DIAS,AUTORIZACION")] PERMISOS pERMISOS)
        {
            if (ModelState.IsValid)
            {
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
            return View("pERMISOS", pERMISOS);
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

        [HttpPost]
        public ActionResult CreateAsc([Bind(Include = "ID_ASCENSO,ID_EMPLEADO,DESCRIPCION,PUESTO_ANT,PUESTO_NVO,FECHA,AUTORIZACION")] ASCENSOS aSCENSOS)
        {
            if (ModelState.IsValid)
            {
                db.ASCENSOS.Add(aSCENSOS);
                db.SaveChanges();
                return RedirectToAction("CreateAsc");
            }

            ViewBagEmpleado();
            return View(aSCENSOS);
        }

        public ActionResult IndexAsc()
        {
            EMPLEADOS Emp = (EMPLEADOS)TempData["Empleado"];
            TempData.Keep("Empleado");
            var aSCENSOS = db.ASCENSOS.Where(v => v.ID_EMPLEADO.Equals(Emp.EMP_ID));
            return View("aSCENSOS", aSCENSOS);
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
                return RedirectToAction("IndexPerm", "Expediente");
            }
        }

        /*******************************************************  Amonestaciones   ******************************************************************/
        /*******************************************************************************************************************************************/
        
                
        
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
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
            }
            else
            {
                ViewBag.ID_EMPLEADO = new SelectList(Empleado, "EMP_ID", "ID_EMPLEADO");
            }
        }

        public void CalcularDiasDisponibles(EMPLEADOS empleado)
        {
            TimeSpan ts = DateTime.Now - empleado.FECHA_CONTR;
            int diferenciaDias = ts.Days;
            int diasDisponibles = (diferenciaDias / 7) / 4 - empleado.DIAS_VAC_UTILIZAD;
            ViewBag.DiasDisponibles = diasDisponibles;
        }

    }
}