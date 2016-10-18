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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

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

        [Required, DisplayName("ID del Empleado")]
        public int ID_EMPLEADO { get; set; }

        [Required, DisplayName("C�dula"), MaxLength(15)]
        public string CEDULA { get; set; }

        [Required, DisplayName("Nombre"), MaxLength(30)]
        public string NOMBRE { get; set; }

        [Required, DisplayName("Primer Apellido"), MaxLength(30)]
        public string APE1 { get; set; }

        [Required, DisplayName("Segundo Apellido"), MaxLength(30)]
        public string APE2 { get; set; }

        [Required, DisplayName("Direcci�n"), MaxLength(100)]
        public string DIRECCION { get; set; }

        [Required, DisplayName("Descripci�n")]
        public string DESCRIPCION { get; set; }

        [Required, DisplayName("Tel�fono de Habitaci�n"), MaxLength(15)]
        public string TEL_HABITACION { get; set; }

        [Required, DisplayName("Tel�fono M�vil"), MaxLength(15)]
        public string TEL_MOVIL { get; set; }

        [Required, DisplayName("Correo Electr�nico"), MaxLength(30)]
        public string E_MAIL { get; set; }

        [Required, DisplayName("Puesto")]
        public int PUESTO { get; set; }

        [Required, DisplayName("Salario")]
        public double SALARIO { get; set; }

        [Required, DisplayName("Estado"), MaxLength(30)]
        public string ESTADO { get; set; }
    
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
