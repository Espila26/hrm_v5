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
    
    public partial class USUARIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIOS()
        {
            this.HISTORIALES = new HashSet<HISTORIALES>();
        }
    
        public int ID_USUARIO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CONTRASEÑA { get; set; }
        public string ESTADO { get; set; }
    
        public virtual EMPLEADOS EMPLEADOS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HISTORIALES> HISTORIALES { get; set; }
    }
}
