using SplitAdminEcomerce.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce
{
    public class PedidoCtrl
    {
        #region Propiedades
        private Splittel Splittel;
        #endregion

        #region Constructores
        public PedidoCtrl(Splittel Splittel)
        {
            this.Splittel = Splittel;
            Splittel.Connect();
            Splittel.LoadObject(Enums.EcomObjects.ViewAd_Pedidos);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Listado de cotizaciones vigentes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ViewAd_Pedidos> GetCotizaciones()
        {
            var Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'C' AND total > 0 AND fecha >= DATE_ADD(CURDATE(), INTERVAL -10 DAY)", " order by fecha desc");
            return Resuslt;
        }
        /// <summary>
        /// Obtiene listado de pedidos completados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ViewAd_Pedidos> GetPedidos()
        {
            var Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'P' AND total > 0 and EstatusEcom = 0", " order by fecha desc");
            return Resuslt;
        }
        /// <summary>
        /// obtiene listado de pedidos pendientes de procesar
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ViewAd_Pedidos> GetPendientes()
        {
            var Resuslt = Splittel.ViewAd_Pedidos.GetOpenquery("where estatus = 'P' AND total > 0 and EstatusEcom <> 0", " order by fecha desc");
            return Resuslt;
        }
        /// <summary>
        /// libera objetos usados
        /// </summary>
        public void Terminar()
        {
            Splittel.ViewAd_Pedidos = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion
    }
}
