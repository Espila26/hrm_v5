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
        public ActionResult Index()
        {
            return View(db.USUARIOS.ToList());
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
                db.SaveChanges();
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
