//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class producto_compra
    {
        public int id { get; set; }
        [Required]
        public Nullable<int> id_compra { get; set; }
        [Required]
        public Nullable<int> id_producto { get; set; }
        [Required]
        public Nullable<int> cantidad { get; set; }
        [Required]
        public virtual compra compra { get; set; }
        [Required]
        public virtual producto producto { get; set; }
    }
}
