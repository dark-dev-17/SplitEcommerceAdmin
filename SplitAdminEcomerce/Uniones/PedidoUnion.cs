using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    public class PedidoUnion
    {
        public Pedido Pedido { get; set; }
        public ViewAd_Clientes Cliente { get; set; }
        public DireccionEnvio DireccionEnvio { get; set; }
        public DireccionFacturacion DireccionFacturacion { get; set; }
        public List<ViewAd_PedidoDetalle> PedidoDetalle { get; set; }
    }
}
