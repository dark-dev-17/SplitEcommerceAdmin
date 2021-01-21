using SplitAdminEcomerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    public class ReporteDocuments
    {
        public string TipoDoc { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public List<Pedido> MyProperty { get; set; }
    }
}
