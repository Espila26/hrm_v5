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
    
    public partial class ASCENSOS
    {
        public int ID_ASCENSO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string DESCRIPCION { get; set; }
        public string PUESTO_ANT { get; set; }
        public int PUESTO_NVO { get; set; }
        public System.DateTime FECHA { get; set; }
        public string AUTORIZACION { get; set; }
    
        public virtual EMPLEADOS EMPLEADOS { get; set; }
        public virtual PUESTOS PUESTOS { get; set; }
    }
}
