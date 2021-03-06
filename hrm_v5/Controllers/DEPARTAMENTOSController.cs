﻿using System;
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
        /*
         El metodo index es el encargado de mostrar los datos de los departamentos en la pantalla principal,
         ademas, proporciona la capacidad de hacer busquedas refinadas que le permiten al usuario manejar facilmente
         la información de los departamentos.
         */
        public ActionResult Index(string searchString) //searchString: parametro de busqueda
        {
            var DEP = from d in db.DEPARTAMENTOS
                      select d;

            //validación para verificar la existencia del criterio de busqueda
            if (!String.IsNullOrEmpty(searchString)) 
            {
                //Muestra los departamentos por el estado que el usuario definió previamente
                if (searchString.Equals("Inactivo") || searchString.Equals("Activo")) 
                {
                    DEP = DEP.Where(s => s.ESTADO.Equals(searchString));
                }

                else if (searchString.Equals("Todo"))
                {
                    DEP = DEP.Where(s => s.ESTADO.Contains("tiv"));
                }

                else if (searchString.Equals("Seleccione"))
                {
                    TempData["Error"] = "¡Debe seleccionar los departamentos que desea ver!";
                    return RedirectToAction("Index");
                }
                
                //Muestra los departamentos que coincidan con el nombre que el usuario desea ver.
                else
                {
                    DEP = DEP.Where(s => s.NOMBRE.Contains(searchString));
                }

                //si no existe registros que coicidan con el criterio de busqueda, se muestra el mensaje de error.
                if (DEP.Count() == 0)
                { 
                    TempData["Error"] = "¡No se encontraron registros coincidentes con el criterio de busqueda!";
                    return RedirectToAction("Index");
                }
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
            viewBagEmpresas();
            return View();
        }

        // POST: DEPARTAMENTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DEPARTAMENTO,NOMBRE,DESCRIPCION,EMPRESA,ESTADO")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.DEPARTAMENTOS.Add(dEPARTAMENTOS);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Se debe de seleccionar una empresa. Si no es posible seleccionar alguna, probablemente, las empresas existentes se encuentren inactivas o no existe ninguna.";
                    return RedirectToAction("Create");
                }
                TempData["Success"] = "¡El Departamento ha sido creado exitosamente!";
                return RedirectToAction("Create");
                }

            viewBagEmpresas();
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
            viewBagEmpresas();
            return View(dEPARTAMENTOS);
        }

        // POST: DEPARTAMENTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DEPARTAMENTO,NOMBRE,DESCRIPCION,EMPRESA,ESTADO")] DEPARTAMENTOS dEPARTAMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEPARTAMENTOS).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Se debe de seleccionar una empresa.Si no es posible seleccionar alguna, probablemente, las empresas existentes se encuentren inactivas o no existe ninguna.";
                    return RedirectToAction("Index");
                }
                db.SaveChanges();
                TempData["Success"] = "¡La información del departamento ha sido editada exitosamente!";
                return RedirectToAction("Index");
            }
            viewBagEmpresas();
            return View(dEPARTAMENTOS);
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
                else if (Request.Form["Inhabilitar"] != null)
                {
                    var PTS = from p in db.PUESTOS
                              select p;
                    var EMPL = from e in db.EMPLEADOS
                               select e;
                    foreach (var i in childChkbox)
                    {
                        var dep = db.DEPARTAMENTOS.Find(Int32.Parse(i));
                        dep.ESTADO = "Inactivo";
                        foreach (var p in PTS)
                        {
                            if (p.DEPARTAMENTO == dep.ID_DEPARTAMENTO)
                            {
                                p.ESTADO = "Inactivo";
                                foreach (var e in EMPL)
                                {
                                    if (e.PUESTO == p.PTS_ID)
                                        e.ESTADO = "Inactivo";
                                }
                            }
                        }
                    db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de el o los Departamentos seleccionados exitosamente!";
                    return RedirectToAction("Index");
                }

                else if (Request.Form["Habilitar"] != null)
                {
                    foreach (var i in childChkbox)
                    {
                        var dep = db.DEPARTAMENTOS.Find(Int32.Parse(i));
                        dep.ESTADO = "Activo";
                        db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de el o los Departamentos seleccionados exitosamente!";
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public void viewBagEmpresas()
        {
            List<object> EMPRESAS = new List<Object>();
            var EMP = db.EMPRESAS;
            foreach (var i in EMP)
            {
                if (i.ESTADO.Equals("Activo"))
                {
                    EMPRESAS.Add(i);
                }
            }
            ViewBag.EMPRESA = new SelectList(EMPRESAS, "ID_EMPRESA", "NOMBRE");
        }

    }
}
