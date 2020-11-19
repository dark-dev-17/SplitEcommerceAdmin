using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbManagerDark;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;

namespace SplitEcommerceAdmin.Controllers
{
    public class PedidoController : Controller
    {
        private PedidoCtrl PedidoCtrl;

        public PedidoController(IConfiguration configuration)
        {
            PedidoCtrl = new PedidoCtrl(new Splittel(configuration));
        }
        // GET: PedidoController
        public ActionResult Cotizacion()
        {
            try
            {
                var result = PedidoCtrl.GetCotizaciones();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PedidoCtrl.Terminar();
                PedidoCtrl = null;
            }
            
        }

        public ActionResult Pedido()
        {
            try
            {
                var result = PedidoCtrl.GetPedidos();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PedidoCtrl.Terminar();
                PedidoCtrl = null;
            }
        }

        public ActionResult Pendiente()
        {
            try
            {
                var result = PedidoCtrl.GetPedidos();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PedidoCtrl.Terminar();
                PedidoCtrl = null;
            }
        }

    }
}
