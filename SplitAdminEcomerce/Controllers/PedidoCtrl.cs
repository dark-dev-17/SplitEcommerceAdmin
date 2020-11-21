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
            if (Splittel.WsB2C == null)
                Splittel.LoadObject(Enums.EcomObjects.WsB2C);
            if (Splittel.OPWebHookLog == null)
                Splittel.LoadObject(Enums.EcomObjects.OPWebHookLog);;
            if (Splittel.WsB2B == null)
                Splittel.LoadObject(Enums.EcomObjects.WsB2B);

            Splittel.Connect(Enums.DbAccess.SapBussinesOne);

            if (Splittel.DireccionPedido == null)
                Splittel.LoadObject(Enums.SapB1Objects.DireccionPedido);
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
                if(pedidoUnion.Pedido.MetodoPago == "03")
                {
                    if(pedidoUnion.Cliente.TipoCliente == "B2C")
                    {
                        //direccion de envio
                        pedidoUnion.DireccionEnvio = Splittel.DireccionEnvio.Get(Int32.Parse(pedidoUnion.Pedido.DatosEnvio));
                        //direccion de facuracion
                        pedidoUnion.DireccionFacturacion = Splittel.DireccionFacturacion.Get(Int32.Parse(pedidoUnion.Pedido.DatosFacturacion));
                        //ultimo log open pay
                        pedidoUnion.OPWebHookLog = Splittel.OPWebHookLog.GetOpenquery($"where t12_f002 = '{IdPedido}' order by t12_f099 desc limit 1");
                        //validar si el log de open pay fue exitoso
                        if (pedidoUnion.OPWebHookLog != null && pedidoUnion.OPWebHookLog.Titulo == "charge.succeeded" && pedidoUnion.OPWebHookLog.Estatus == "completed")
                        {
                            pedidoUnion.WsB2C = Splittel.WsB2C.GetOpenquery($"where t06_f006 = '{IdPedido}'");
                        }
                    }
                    else
                    {
                        //direccion de envio
                        pedidoUnion.DirEnvioB2B = Splittel.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'S'", pedidoUnion.Cliente.CardCode.Trim())).Find(a => a.Nombre == pedidoUnion.Pedido.DatosEnvio.Trim());
                        //direccion de facuracion
                        pedidoUnion.DirFacturacionB2B = Splittel.DireccionPedido.GetSpecialStat(string.Format("exec Eco_GetAddressByCustomer @CardCode = '{0}', @AdresType = 'B'", pedidoUnion.Cliente.CardCode.Trim())).Find(a => a.Nombre == pedidoUnion.Pedido.DatosFacturacion.Trim());
                        //ultimo log open pay
                        pedidoUnion.OPWebHookLog = Splittel.OPWebHookLog.GetOpenquery($"where t12_f002 = '{IdPedido}' order by t12_f099 desc limit 1");
                        //validar si el log de open pay fue exitoso
                        if (pedidoUnion.OPWebHookLog != null && pedidoUnion.OPWebHookLog.Titulo == "charge.succeeded" && pedidoUnion.OPWebHookLog.Estatus == "completed")
                        {
                            pedidoUnion.WsB2B = Splittel.WsB2B.GetOpenquery($"where t06_f005 = '{IdPedido}'");
                        }
                    }
                }
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
