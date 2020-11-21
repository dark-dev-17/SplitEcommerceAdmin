using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.ModelsSAP;
using SplitAdminEcomerce.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    public class PedidoUnion
    {
        /// <summary>
        /// descripcion del pedido
        /// </summary>
        public Pedido Pedido { get; set; }
        /// <summary>
        /// detalle del cliente
        /// </summary>
        public ViewAd_Clientes Cliente { get; set; }
        /// <summary>
        /// direccion de envio para casos B2C
        /// </summary>
        public DireccionEnvio DireccionEnvio { get; set; }
        /// <summary>
        /// Direccion de facturacion para casos B2C
        /// </summary>
        public DireccionFacturacion DireccionFacturacion { get; set; }
        /// <summary>
        /// direccion de envio para casos B2B
        /// </summary>
        public DireccionPedido DirEnvioB2B { get; set; }
        /// <summary>
        /// Direccion de facturacion para casos B2B
        /// </summary>
        public DireccionPedido DirFacturacionB2B { get; set; }
        /// <summary>
        /// proceso web service B2C
        /// </summary>
        public WsB2C WsB2C { get; set; }
        /// <summary>
        /// proceso web service B2B
        /// </summary>
        public WsB2B WsB2B { get; set; }
        /// <summary>
        /// Ultimo log para pagos con tajeta
        /// </summary>
        public OPWebHookLog OPWebHookLog { get; set; }
        /// <summary>
        /// lista de partidas
        /// </summary>
        public List<ViewAd_PedidoDetalle> PedidoDetalle { get; set; }

    }
}
