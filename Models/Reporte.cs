using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Reporte
    {

        public String nombreProveedor { set; get; }

        public String direccionProveedor { set; get; }

        public String telefonoProveedor { set; get; }

        public String nombreProducto { set; get; }

        public int? percioProducto { set; get; }

        public String nombreCliente { set; get; }

        public String emailCliente { set; get; }

        public int? idCompra { set; get; }
        
        public int? totalCompra { set; get; }

    }

}