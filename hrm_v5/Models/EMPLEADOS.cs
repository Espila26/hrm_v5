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
    
    public partial class EMPLEADOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMPLEADOS()
        {
            this.AMONESTACIONES = new HashSet<AMONESTACIONES>();
            this.ASCENSOS = new HashSet<ASCENSOS>();
            this.HISTORIALES = new HashSet<HISTORIALES>();
            this.PERMISOS = new HashSet<PERMISOS>();
            this.SUSPENSIONES = new HashSet<SUSPENSIONES>();
            this.USUARIOS = new HashSet<USUARIOS>();
            this.VACACIONES = new HashSet<VACACIONES>();
        }
    
        public int EMP_ID { get; set; }
        public string ID_EMPLEADO { get; set; }
        public string CEDULA { get; set; }
        public string NOMBRE { get; set; }
        public string APE1 { get; set; }
        public string APE2 { get; set; }
        public string DIRECCION { get; set; }
        public string DESCRIPCION { get; set; }
        public string TEL_HABITACION { get; set; }
        public string TEL_MOVIL { get; set; }
        public string E_MAIL { get; set; }
        public int PUESTO { get; set; }
        public double SALARIO { get; set; }
        public string ESTADO { get; set; }
        public System.DateTime FECHA_NAC { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AMONESTACIONES> AMONESTACIONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASCENSOS> ASCENSOS { get; set; }
        public virtual PUESTOS PUESTOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORIALES> HISTORIALES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PERMISOS> PERMISOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUSPENSIONES> SUSPENSIONES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<USUARIOS> USUARIOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VACACIONES> VACACIONES { get; set; }
    }
}
