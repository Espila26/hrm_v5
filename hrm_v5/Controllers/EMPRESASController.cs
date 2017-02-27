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
    public class EMPRESASController : Controller
    {
        private Entities db = new Entities();

        // GET: EMPRESAS
        /*
        El metodo index es el encargado de mostrar los datos de las Empresas en la pantalla principal,
        ademas, proporciona la capacidad de hacer busquedas refinadas que le permiten al usuario manejar facilmente
        la información de los Empresas.
        */
        public ActionResult Index(string searchString)
        {
            var EMP = from e in db.EMPRESAS
                      select e;

            //validación para verificar la existencia del criterio de busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                //Muestra las empresas por el estado que el usuario definió previamente
                if (searchString.Equals("Inactivo") || searchString.Equals("Activo"))
                {
                    EMP = EMP.Where(s => s.ESTADO.Equals(searchString));
                }

                else if (searchString.Equals("Todo"))
                {
                    EMP = EMP.Where(s => s.ESTADO.Contains("tiv"));
                }

                else if (searchString.Equals("Seleccione"))
                {
                    TempData["Error"] = "¡Debe seleccionar las empresas que desea ver!";
                    return RedirectToAction("Index");
                }

                //Muestra las empresas que coincidan con el nombre, apellidos o cedula que el usuario desea ver.
                else
                {
                    EMP = EMP.Where(s => s.NOMBRE.Contains(searchString));
                }

                //si no existe registros que coicidan con el criterio de busqueda, se muestra el mensaje de error.
                if (EMP.Count() == 0)
                {
                    TempData["Error"] = "¡No se encontraron registros coincidentes con el criterio de busqueda!";
                    return RedirectToAction("Index");
                }
            }

            return View(EMP);
        }

        // GET: EMPRESAS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            if (eMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESAS);
        }

        // GET: EMPRESAS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EMPRESAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_EMPRESA,NOMBRE,RAZON_SOCIAL,CEDULA_JURIDICA,FECHA_FUNDACION,PAIS_ORIGEN,SEDE_CENTRAL,ESTADO")] EMPRESAS eMPRESAS)
        {
            if (ModelState.IsValid)
            {
                db.EMPRESAS.Add(eMPRESAS);
                db.SaveChanges();
                TempData["Success"] = "¡La empresa ha sido creada exitosamente!";
                return RedirectToAction("Create");
            }

            return View(eMPRESAS);
        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos una empresa!";
            }
            else
            {
                if (Request.Form["Detalles"] != null)
                {
                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Details", "EMPRESAS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "!Solamente es posible ver los detalles de una empresa a la vez!";
                    }
                }
                else if (Request.Form["Editar"] != null)
                {

                    if (childChkbox.Count() == 1)
                    {
                        return RedirectToAction("Edit", "EMPRESAS", new { id = childChkbox.First() });
                    }
                    else
                    {
                        TempData["Error"] = "¡Solamente es posible editar una empresa a la vez!";
                    }
                }
                else if(Request.Form["Inhabilitar"] != null)
                {
                    var DEP = from d in db.DEPARTAMENTOS
                              select d;
                    var PTS = from p in db.PUESTOS
                              select p;
                    var EMPL = from e in db.EMPLEADOS
                              select e;
                    foreach (var i in childChkbox)
                    {
                        var emp = db.EMPRESAS.Find(Int32.Parse(i));
                        emp.ESTADO = "Inactivo";
                        foreach(var d in DEP)
                        {
                            if (d.EMPRESA == emp.ID_EMPRESA)
                            {
                                d.ESTADO = "Inactivo";
                                foreach (var p in PTS)
                                {
                                    if (p.DEPARTAMENTO == d.ID_DEPARTAMENTO)
                                    {
                                        p.ESTADO = "Inactivo";
                                        foreach (var e in EMPL)
                                        {
                                            if(e.PUESTO==p.PTS_ID)
                                                e.ESTADO = "Inactivo";
                                        }
                                    }
                                }
                            }

                        }
                        db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de la o las empresas seleccionadas exitosamente!";
                }

                else if (Request.Form["Habilitar"] != null)
                {
                    foreach (var i in childChkbox)
                    {
                        var emp = db.EMPRESAS.Find(Int32.Parse(i));
                        emp.ESTADO = "Activo";
                        db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de la o las empresas seleccionadas exitosamente!";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: EMPRESAS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPRESAS eMPRESAS = db.EMPRESAS.Find(id);
            if (eMPRESAS == null)
            {
                return HttpNotFound();
            }
            return View(eMPRESAS);
        }

        // POST: EMPRESAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EMPRESA,NOMBRE,RAZON_SOCIAL,CEDULA_JURIDICA,FECHA_FUNDACION,PAIS_ORIGEN,SEDE_CENTRAL,ESTADO")] EMPRESAS eMPRESAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPRESAS).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "¡La informacion de la Empresa ha sido editada exitosamente!";
                return RedirectToAction("Index");
            }
            return View(eMPRESAS);
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