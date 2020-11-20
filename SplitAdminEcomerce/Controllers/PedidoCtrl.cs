using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Uniones;
using SplitAdminEcomerce.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Controllers
{
    public class PedidoCtrl
    {
        #region Propiedades
        public Splittel Splittel { get; internal set; }
        #endregion

        #region Constructores
        public PedidoCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();
            if(Splittel.ViewAd_Pedidos == null)
                Splittel.LoadObject(Enums.EcomObjects.ViewAd_Pedidos);
            if(Splittel.Pedido == null)
                Splittel.LoadObject(Enums.EcomObjects.Pedido);
            if (Splittel.ViewAd_PedidoDetalle == null)
                Splittel.LoadObject(Enums.EcomObjects.ViewAd_PedidoDetalle);
            if (Splittel.DireccionFacturacion == null)
                Splittel.LoadObject(Enums.EcomObjects.DireccionFacturacion);
            if (Splittel.DireccionEnvio == null)
                Splittel.LoadObject(Enums.EcomObjects.DireccionEnvio);
        }
        #endregion

        #region Metodos
        public PedidoUnion GetPedido(int IdPedido)
        {
            PedidoUnion pedidoUnion = new PedidoUnion();
            pedidoUnion.Pedido = Splittel.Pedido.Get(IdPedido);
            if(pedidoUnion.Pedido is null)
            {
                throw new SplitException { Category = TypeException.Info, Description = "No se encontro el pedido seleccionado", ErrorCode = 100 };
            }
            pedidoUnion.PedidoDetalle = Splittel.ViewAd_PedidoDetalle.GetOpenquery($"where id_cotizacion = '{IdPedido}'", "order by fecha_registro desc");
            pedidoUnion.Cliente = new ClienteCtrl(Splittel).GetView(pedidoUnion.Pedido.IdCliente);
            if(pedidoUnion.Pedido.Estatus == "P")
            {
                pedidoUnion.DireccionEnvio = Splittel.DireccionEnvio.Get(Int32.Parse(pedidoUnion.Pedido.DatosEnvio));
                pedidoUnion.DireccionFacturacion = Splittel.DireccionFacturacion.Get(Int32.Parse(pedidoUnion.Pedido.DatosFacturacion));
            }
            return pedidoUnion;
        }
        /// <summary>
        /// Listado de cotizaciones vigentes
        /// </summary>
        /// <param name="IdCliente">Id de cliente enviar 0 cuando se desee extraer sin filtro por cliente</param>
        /// <returns></returns>
        public List<ViewAd_Pedidos> GetCotizaciones(int IdCliente = 0)
        {
            var Resuslt = new List<ViewAd_Pedidos>();
            if (IdCliente == 0)
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'C' AND total > 0 AND fecha >= DATE_ADD(CURDATE(), INTERVAL -20 DAY)", " order by fecha desc");
            }
            else
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery(string.Format("where id_cliente = '{0}' and estatus = 'C' AND total > 0 AND fecha >= DATE_ADD(CURDATE(), INTERVAL -20 DAY)",IdCliente), " order by fecha desc");
            }
            return Resuslt;
        }
        /// <summary>
        /// Obtiene listado de pedidos completados
        /// </summary>
        /// <param name="IdCliente">Id de cliente enviar 0 cuando se desee extraer sin filtro por cliente</param>
        /// <returns></returns>
        public List<ViewAd_Pedidos> GetPedidos(int IdCliente = 0)
        {
            var Resuslt = new List<ViewAd_Pedidos>();
            if (IdCliente == 0)
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'P' AND total > 0 and EstatusWS = 0", " order by fecha desc");
            }
            else
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery(string.Format("where id_cliente = '{0}' and estatus = 'P' AND total > 0 and EstatusWS = 0", IdCliente), " order by fecha desc");
            }
            return Resuslt;
        }
        /// <summary>
        /// obtiene listado de pedidos pendientes de procesar
        /// </summary>
        /// <param name="IdCliente">Id de cliente enviar 0 cuando se desee extraer sin filtro por cliente</param>
        /// <returns></returns>
        public List<ViewAd_Pedidos> GetPendientes(int IdCliente = 0)
        {
            var Resuslt = new List<ViewAd_Pedidos>();
            if (IdCliente == 0)
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'P' AND total > 0 and EstatusWS <> 0", " order by fecha desc");
            }
            else
            {
                Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery(string.Format("where id_cliente = '{0}' and estatus = 'P' AND total > 0 and EstatusWS <> 0", IdCliente), " order by fecha desc");
            }
            return Resuslt;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.ViewAd_Pedidos = null;
            Splittel.DisConnect();
            Splittel.Terminar();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
