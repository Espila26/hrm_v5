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
                if (EMP.Count() == 0)
                {
                    TempData["Error"] = "¡Los datos ingresados no pertenecen a ningún empleado asociado a la empresa!";
                    return RedirectToAction("Index");

                }
                
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
            viewBagPuestos();
            return View();
        }

        // POST: EMPLEADOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMP_ID,ID_EMPLEADO,CEDULA,NOMBRE,APE1,APE2,DIRECCION,DESCRIPCION,TEL_HABITACION,TEL_MOVIL,E_MAIL,PUESTO,SALARIO,ESTADO,FECHA_NAC")] EMPLEADOS eMPLEADOS)
        {
            if (ModelState.IsValid)
            {
                db.EMPLEADOS.Add(eMPLEADOS);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Se debe de seleccionar un puesto.Si no es posible seleccionar alguno, probablemente, los puestos existentes se encuentren inactivas o no existe ninguno.";
                    return RedirectToAction("Create");
                }
                TempData["Success"] = "¡El empleado ha sido creado exitosamente!";
                return RedirectToAction("Create");
            }

            ViewBag.PUESTO = new SelectList(db.PUESTOS, "PTS_ID", "NOMBRE", eMPLEADOS.PUESTO);
            return View(eMPLEADOS);

        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un empleado!";
                return RedirectToAction("Index");
            }
            else
            {
                if (Request.Form["Detalles"] != null)
                {
                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Details", "EMPLEADOS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible ver los detalles de un empleado a la vez!";
                        return RedirectToAction("Index");
                    }
                }
                else if (Request.Form["Editar"] != null)
                {

                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Edit", "EMPLEADOS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible editar un empleado a la vez!";
                        return RedirectToAction("Index");
                    }
                }
                else if (Request.Form["Inhabilitar"] != null)
                {
                    foreach (var i in childChkbox)
                    {
                        var emp = db.EMPLEADOS.Find(Int32.Parse(i));
                        emp.ESTADO = "Inactivo";
                        db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de el o los Empleados seleccionados exitosamente!";
                    return RedirectToAction("Index");
                }

                else if (Request.Form["Habilitar"] != null)
                {
                    int cantNoActualiza = 0;

                    foreach (var i in childChkbox)
                    {
                        var emp = db.EMPLEADOS.Find(Int32.Parse(i));
                        var pts = db.PUESTOS.Find(emp.PUESTOS.PTS_ID);

                        if (pts.ESTADO.Equals("Activo"))
                        {
                            emp.ESTADO = "Activo";
                            db.SaveChanges();
                        }
                        else
                        {
                            cantNoActualiza++;
                        }
                    }

                    if (cantNoActualiza == 0)
                        TempData["Success"] = "¡Se ha cambiado el estado de el o los Empleados seleccionados exitosamente!";
                    else
                        TempData["Error"] = " El estado de Algun(os) empleado(os) no a podido ser actualizado, debido a que el puesto a la que pertenece(en) se encuentra inactivo";

                    return RedirectToAction("Index");
                }
                return View();
            }
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
            viewBagPuestos();
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Se debe de seleccionar un puesto.Si no es posible seleccionar alguno, probablemente, los puestos existentes se encuentren inactivas o no existe ninguno.";
                    return RedirectToAction("Index");
                }
                TempData["Success"] = "¡La información del empleado ha sido editada exitosamente!";
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

        public string CrearID(){
            int cont = 0;
            string dia = @DateTime.Now.Day.ToString();
            string mes = @DateTime.Now.Month.ToString();
            string año = DateTime.Now.Year.ToString();
            string fecha = dia + mes + año;
            if (db.EMPLEADOS.Count() == 0)
            {
                return cont + "-" + fecha;
            }
            else
            {
                while (cont != db.EMPLEADOS.Count())
                {
                    cont++;
                    cont.ToString();
                }
                return cont + "-" + fecha;
            }
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string UserName)
        {

            var user = from e in db.EMPLEADOS
                      select e;

            if (!String.IsNullOrEmpty(UserName))
            {
                user = user.Where(s => s.NOMBRE.Contains(UserName));
            }

            return Json(user == null);
        }

        public void viewBagPuestos()
        {
            List<object> PUESTOS = new List<Object>();
            var PTS = db.PUESTOS;
            foreach (var i in PTS)
            {
                var DEP = db.DEPARTAMENTOS.Find(i.DEPARTAMENTO);
                var EMP = db.EMPRESAS.Find(DEP.EMPRESA);
                if (EMP.ESTADO.Equals("Activo"))
                {
                    PUESTOS.Add(i);
                }
            }
            ViewBag.PUESTO = new SelectList(PUESTOS, "PTS_ID", "NOMBRE");
        }
    }
}
