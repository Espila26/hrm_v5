//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace hrm_v5.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class VACACIONES
    {
        public int ID_SOLICITUD { get; set; }
        public int ID_EMPLEADO { get; set; }
        public System.DateTime INICIO { get; set; }
        public System.DateTime FINAL { get; set; }
        public int CANT_DIAS { get; set; }
        public string AUTORIZACION { get; set; }
    
        public virtual EMPLEADOS EMPLEADOS { get; set; }
    }
}
