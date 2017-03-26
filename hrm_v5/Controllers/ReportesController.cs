using hrm_v5.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

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
            var generarDesde = GenerarDesde(desde);
            var generarHasta = GenerarHasta(hasta);

            TempData["datoEmpleado"] = datoEmpleado;
            TempData["estado"] = Estado;
            TempData["desde"] = desde;
            TempData["hasta"] = hasta;
            TempData["accion"] = accion;

            if (accion.Equals("Vacaciones"))
            {
                var Vac = from e in db.VACACIONES
                          select e;

                Vac = Vac.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                return View("ReporteVacaciones", Vac);
            }

            else if(accion.Equals("Amonestaciones")){

                var Amon = from e in db.AMONESTACIONES
                          select e;

                Amon = Amon.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                return View("ReporteAmonestaciones", Amon);

            }

            else if (accion.Equals("Ascensos"))
            {

                var Asc = from e in db.ASCENSOS
                           select e;

                Asc = Asc.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                return View("ReporteAscensos", Asc);

            }

            else if (accion.Equals("Permisos"))
            {

                var Per = from e in db.PERMISOS
                          select e;

                Per = Per.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                return View("ReportePermisos", Per);

            }
            else if (accion.Equals("Suspensiones"))
            {

                var Sus = from e in db.SUSPENSIONES
                          select e;

                Sus = Sus.Where(s => (s.EMPLEADOS.NOMBRE.Contains(datoEmpleado) || s.EMPLEADOS.APE1.Contains(datoEmpleado) || s.EMPLEADOS.APE2.Contains(datoEmpleado) || s.EMPLEADOS.CEDULA.Contains(datoEmpleado)) && (s.EMPLEADOS.ESTADO.Contains(Estado)) && (s.FECHA_CREACION >= generarDesde) && (s.FECHA_CREACION <= generarHasta));
                return View("ReporteSuspensiones", Sus);

            }

            else
            {

                var Emp = from e in db.EMPLEADOS
                           select e;

                Emp = Emp.Where(s => (s.NOMBRE.Contains(datoEmpleado) || s.APE1.Contains(datoEmpleado) || s.APE2.Contains(datoEmpleado) || s.CEDULA.Contains(datoEmpleado)) && (s.ESTADO.Contains(Estado)) && (s.FECHA_CONTR >= generarDesde) && (s.FECHA_CONTR <= generarHasta));
                return View("ReporteEmpleados", Emp);

            }
        }

        public void ExportarExcel()
        {

            string gridData = "";

            TempData.Keep("Accion");
            switch (TempData["Accion"].ToString())
            {
                case "Vacaciones":
                    gridData = gridVacaciones();
                    break;
                case "Amonestaciones":
                    gridData = gridAmonestaciones();
                    break;
                case "Ascensos":
                    gridData = gridAscensos();
                    break;
                case "Permisos":
                    gridData = gridPermisos();
                    break;
                case "Suspensiones":
                    gridData = gridSuspensiones();
                    break;
                default:
                    gridData = gridEmpleados();
                    break;
            }

            Response.ClearContent();         
            Response.AddHeader("content-disposition", "attachment; filename=ReporteVacaciones.xls");
            Response.ContentType = "application/excel";
            Response.Write(gridData);
            Response.End();
        }

        public FileStreamResult ExportarPDF()
        {

            string gridData = "";
            TempData.Keep("Accion");

            switch (TempData["Accion"].ToString())
            {
                case "Vacaciones":
                    gridData = gridVacaciones();
                    break;
                case "Amonestaciones":
                    gridData = gridAmonestaciones();
                    break;
                case "Ascensos":
                    gridData = gridAscensos();
                    break;
                case "Permisos":
                    gridData = gridPermisos();
                    break;
                case "Suspensiones":
                    gridData = gridSuspensiones();
                    break;
                default:
                    gridData = gridEmpleados();
                    break;
            }

            string exportData = String.Format("<html><head>{0}</head><body>{1}</body></html>", "<style>table{ border-spacing: 10px; border-collapse: separate; }</style>", gridData);
            var bytes = System.Text.Encoding.UTF8.GetBytes(exportData);

            using (var input = new MemoryStream(bytes))
            {
                var output = new MemoryStream();
                var document = new iTextSharp.text.Document(PageSize.A4, 50, 50, 50, 50);
                var writer = PdfWriter.GetInstance(document, output);
                writer.CloseStream = false;
                document.Open();

                var xmlWorker = iTextSharp.tool.xml.XMLWorkerHelper.GetInstance();
                xmlWorker.ParseXHtml(writer, document, input, System.Text.Encoding.UTF8);
                document.Close();
                output.Position = 0;
                return new FileStreamResult(output, "application/pdf");
            }
        }

        public string gridVacaciones()
        {
            List<VACACIONES> listVacaciones = new List<VACACIONES>();

            listVacaciones = db.VACACIONES.ToList();

            WebGrid grid = new WebGrid(source: listVacaciones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                            grid.Column("ID_SOLICITUD", "Número de Solicitud: "),
                            grid.Column("ID_EMPLEADO", "Número de Empleado: "),
                            grid.Column("INICIO", "Fecha de Inicio: "),
                            grid.Column("FINAL", "Fecha de Ingreso: "),
                            grid.Column("CANT_DIAS", "Cantidad de Dias: "),
                            grid.Column("AUTORIZACION", "Autorización: ")
                            )).ToString();
            return gridData;
        }

        public string gridAmonestaciones()
        {
            List<AMONESTACIONES> Amonestaciones = new List<AMONESTACIONES>();

            Amonestaciones = db.AMONESTACIONES.ToList();

            WebGrid grid = new WebGrid(source: Amonestaciones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_AMONESTACION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("FECHA_INICIO", "Fecha de Inicio"),
                     grid.Column("FECHA_FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo:"),
                     grid.Column("GOCE_SALARIO", "Repercución de la Amonestación"),
                     grid.Column("VERB_ESC", "Tipo de Amonestacion"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridAscensos()
        {
            List<ASCENSOS> Ascensos = new List<ASCENSOS>();

            Ascensos = db.ASCENSOS.ToList();

            WebGrid grid = new WebGrid(source: Ascensos, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_AMONESTACION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("FECHA_INICIO", "Fecha de Inicio"),
                     grid.Column("FECHA_FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo:"),
                     grid.Column("GOCE_SALARIO", "Repercución de la Amonestación"),
                     grid.Column("VERB_ESC", "Tipo de Amonestacion"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
       }

        public string gridPermisos()
        {
            List<PERMISOS> Permisos = new List<PERMISOS>();

            Permisos = db.PERMISOS.ToList();

            WebGrid grid = new WebGrid(source: Permisos, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_PERMISO", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("INICIO", "Fecha de Inicio"),
                     grid.Column("FINAL", "Fecha de Ingreso"),
                     grid.Column("GOCE_SALARIO", "Tipo de Permiso"),
                     grid.Column("CANT_HORAS", "Cantidad de Horas"),
                     grid.Column("CANT_DIAS", "Cantidad de Días"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridSuspensiones()
        {
            List<SUSPENSIONES> Suspensiones = new List<SUSPENSIONES>();

            Suspensiones = db.SUSPENSIONES.ToList();

            WebGrid grid = new WebGrid(source: Suspensiones, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("ID_SUSPENSION", "Número de Solicitud"),
                     grid.Column("ID_EMPLEADO", "Empleado"),
                     grid.Column("INICIO", "Fecha de Inicio"),
                     grid.Column("FINAL", "Fecha de Ingreso"),
                     grid.Column("DESCRIPCION", "Motivo"),
                     grid.Column("GOCE_SALARIO", "Tipo de Suspensión"),
                     grid.Column("AUTORIZACION", "Autorización")
                            )).ToString();
            return gridData;
        }

        public string gridEmpleados()
        {
            List<EMPLEADOS> Empleados = new List<EMPLEADOS>();

            Empleados = db.EMPLEADOS.ToList();

            WebGrid grid = new WebGrid(source: Empleados, canPage: false, canSort: false);

            string gridData = grid.GetHtml(
            columns: grid.Columns(
                     grid.Column("CEDULA", "Cédula"),
                     grid.Column("NOMBRE", "Nombre"),
                     grid.Column("APE1", "Primer Apellido"),
                     grid.Column("APE2", "Segundo Apellido"),
                     grid.Column("TEL_MOVIL", "Teléfono Móvil"),
                     grid.Column("E_MAIL", "Correo Electrónico"),
                     grid.Column("ESTADO", "Estado (Activo o Inactivo)")
                            )).ToString();
            return gridData;
        }

        public DateTime GenerarDesde(string desde)
        {
            var generarDesde = new DateTime();

            if (String.IsNullOrEmpty(desde))
            {

                generarDesde = new DateTime(2017, 3, 17);
            }
            else
            {

                generarDesde = DateTime.Parse(desde);
            }

            return generarDesde;
        }

        public DateTime GenerarHasta(string hasta)
        {
            var generarHasta = new DateTime();

            if (String.IsNullOrEmpty(hasta))
            {

                generarHasta = new DateTime(2099, 3, 19);
            }
            else
            {
                generarHasta = DateTime.Parse(hasta);
            }

            return generarHasta;
        }
    }
}