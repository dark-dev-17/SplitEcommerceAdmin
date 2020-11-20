using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Tools;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Exceptions;

namespace SplitEcommerceAdmin.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteCtrl ClienteCtrl;

        public ClienteController(IConfiguration configuration)
        {
            ClienteCtrl = new ClienteCtrl(new Splittel(configuration));
        }

        // GET: ClienteController
        public ActionResult Index()
        {
            try
            {
                var result = ClienteCtrl.GetClientes();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ClienteCtrl.Terminar();
                ClienteCtrl = null;
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ClienteCtrl.GetView(Int32.Parse(idDecripted));
                if(result == null)
                {
                    throw new SplitException { Category = TypeException.Info, Description = "No se encontro el cliente seleccionado", ErrorCode = 100 };
                }
                PedidoCtrl pedidoCtrl = new PedidoCtrl(ClienteCtrl.Splittel);
                ViewData["Cotizaciones"] = pedidoCtrl.GetCotizaciones(result.IdCliente);
                ViewData["Pedidos"] = pedidoCtrl.GetPedidos(result.IdCliente);
                ViewData["Pendientes"] = pedidoCtrl.GetPendientes(result.IdCliente);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ClienteCtrl.Terminar();
                ClienteCtrl = null;
            }
        }
    }
}
