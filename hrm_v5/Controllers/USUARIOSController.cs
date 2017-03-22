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
    public class USUARIOSController : Controller
    {
        private Entities db = new Entities();

        // GET: USUARIOS
        public ActionResult Index(string searchString)
        {
            var USR = from e in db.USUARIOS
                      select e;

            //validación para verificar la existencia del criterio de busqueda
            if (!String.IsNullOrEmpty(searchString))
            {
                //Muestra los empleados por el estado que el usuario definió previamente
                if (searchString.Equals("Inactivo") || searchString.Equals("Activo"))
                {
                    USR = USR.Where(s => s.ESTADO.Equals(searchString));
                }

                else if (searchString.Equals("Todo"))
                {
                    USR = USR.Where(s => s.ESTADO.Contains("tiv"));
                }

                else if (searchString.Equals("Seleccione"))
                {
                    TempData["Error"] = "¡Debe seleccionar los empleados que desea ver!";
                    return RedirectToAction("Index");
                }

                //Muestra los empleados que coincidan con el nombre, apellidos o cedula que el usuario desea ver.
                else
                {
                    USR = USR.Where(s => s.NOMBRE_USUARIO.Contains(searchString));
                }

                //si no existe registros que coicidan con el criterio de busqueda, se muestra el mensaje de error.
                if (USR.Count() == 0)
                {
                    TempData["Error"] = "¡No se encontraron registros coincidentes con el criterio de busqueda!";
                    return RedirectToAction("Index");

                }

            }

            return View(USR);
        }

        // GET: USUARIOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USUARIOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_USUARIO,NOMBRE_USUARIO,CONTRASEÑA,ESTADO,ROLE,ACC_EMPRESA,ACC_DEPART,ACC_PUESTOS,ACC_EMPLEADOS,ACC_ACCIONES,ACC_USUARIO")] USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                db.USUARIOS.Add(uSUARIOS);
                TempData["Error"] = uSUARIOS.ROL+ ": " + uSUARIOS.ACC_ACCIONES + ": " + uSUARIOS.ACC_DEPART + ": " + uSUARIOS.ACC_EMPRESA + ": " + uSUARIOS.ACC_PUESTOS + ": " + uSUARIOS.ACC_EMPLEADOS + ": " + uSUARIOS.ACC_USUARIO + ": ";
                return RedirectToAction("Index");
            }

            return View(uSUARIOS);
        }

        // GET: USUARIOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_USUARIO,NOMBRE_USUARIO,CONTRASEÑA,ESTADO,ROLE,ACC_EMPRESA,ACC_DEPART,ACC_PUESTOS,ACC_EMPLEADOS,ACC_ACCIONES,ACC_USUARIO")] USUARIOS uSUARIOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            db.USUARIOS.Remove(uSUARIOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult formAction(string[] childChkbox)
        {
            if (childChkbox == null)
            {
                TempData["Error"] = "¡Se debe seleccionar al menos un usuario!";
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
                        TempData["Error"] = "¡Solamente es posible ver los detalles de un usuario a la vez!";
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
                        TempData["Error"] = "¡Solamente es posible editar un usuario a la vez!";
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
                    TempData["Success"] = "¡Se ha cambiado el estado de el o los Usuarios seleccionados exitosamente!";
                    return RedirectToAction("Index");
                }

                else if (Request.Form["Habilitar"] != null)
                {
                    foreach (var i in childChkbox)
                    {
                        var emp = db.EMPLEADOS.Find(Int32.Parse(i));
                        emp.ESTADO = "Activo";
                        db.SaveChanges();
                    }
                    TempData["Success"] = "¡Se ha cambiado el estado de el o los Usuarios seleccionados exitosamente!";
                    return RedirectToAction("Index");
                }
                return View();
            }
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
