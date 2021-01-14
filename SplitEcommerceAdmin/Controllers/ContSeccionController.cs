using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using SplitAdminEcomerce.Uniones.Extras;
using SplitEcommerceAdmin.Models;

namespace SplitEcommerceAdmin.Controllers
{
    public class ContSeccionController : Controller
    {
        private ContSeccionCtrl ContSeccionCtrl;

        public ContSeccionController(IConfiguration configuration)
        {
            ContSeccionCtrl = new ContSeccionCtrl(new Splittel(configuration));
        }

        public IActionResult Index()
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                var result =ContSeccionCtrl.GetCont_Seccions();
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound();
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        public IActionResult Details(string id)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ContSeccionCtrl.GetCont_Seccion(Int32.Parse(idDecripted));
                if (result is null)
                {
                    return NotFound();
                }
                ViewData["Files"] = ContSeccionCtrl.Get_SeccionArchivos(Int32.Parse(idDecripted));
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound();
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        public IActionResult Edit(string id)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ContSeccionCtrl.GetCont_Seccion(Int32.Parse(idDecripted));
                if (result is null)
                {
                    return NotFound();
                }
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound();
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cont_Seccion Cont_Seccion)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Cont_Seccion);
                }
                ContSeccionCtrl.AddUpdSeccion(Cont_Seccion);
                ViewData["MessajeSuccess"] = "La sección de contenido ha sido actualizada exitosamente";
                return View(Cont_Seccion);
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return View(Cont_Seccion);
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cont_Seccion Cont_Seccion)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Cont_Seccion);
                }
                int result = ContSeccionCtrl.AddUpdSeccion(Cont_Seccion);
                ViewData["MessajeSuccess"] = "La sección de contenido ha sido creada exitosamente";
                return RedirectToAction("Details", new { id = EncryptData.Encrypt(result + "") });
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return View(Cont_Seccion);
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        public IActionResult Create()
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            return View(new Cont_Seccion());
        }

        public IActionResult AgregarFile(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ContSeccionCtrl.GetCont_Seccion(Int32.Parse(idDecripted));
                if (result is null)
                {
                    return NotFound();
                }
                ViewData["Seccion"] = result;
                return View(new Cont_SeccionArchivo { IdCont_Seccion = result.IdCont_Seccion });
            }
            catch (SplitException ex)
            {
                return NotFound();
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarFile(Cont_SeccionArchivo Cont_SeccionArchivo)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            ViewData["Seccion"] = ContSeccionCtrl.GetCont_Seccion(Cont_SeccionArchivo.IdCont_Seccion); 
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(Cont_SeccionArchivo);
                }
                ContSeccionCtrl.AddSeccionFile(Cont_SeccionArchivo);
                ViewData["MessajeSuccess"] = "Se ha agregado una nueva imagen";
                return RedirectToAction("Details", new { id = EncryptData.Encrypt(Cont_SeccionArchivo.IdCont_Seccion + "") });
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return View(Cont_SeccionArchivo);
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        public IActionResult EditarFile(string idSecc, string IdFile)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            try
            {
                string idDecripted = EncryptData.Decrypt(idSecc);
                var result = ContSeccionCtrl.Get_SeccionArchivo(Int32.Parse(idDecripted), Int32.Parse(EncryptData.Decrypt(IdFile)));
                if (result is null)
                {
                    return NotFound();
                }
                ViewData["Seccion"] = ContSeccionCtrl.GetCont_Seccion(Int32.Parse(idDecripted)); ;
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound();
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarFile(Cont_SeccionArchivo Cont_SeccionArchivo)
        {
            ViewData["SiteBase"] = ContSeccionCtrl.Splittel.FtpServ.FtpSitebase;
            ViewData["Seccion"] = ContSeccionCtrl.GetCont_Seccion(Cont_SeccionArchivo.IdCont_Seccion);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Cont_SeccionArchivo);
                }
                ContSeccionCtrl.EditSeccionFile(Cont_SeccionArchivo);
                ViewData["MessajeSuccess"] = "Se ha agregado una nueva imagen";
                return RedirectToAction("Details", new { id = EncryptData.Encrypt(Cont_SeccionArchivo.IdCont_Seccion + "") });
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return View(Cont_SeccionArchivo);
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }

        [HttpPost]
        public IActionResult EditarFilePosiciones([FromBody]List<ElementoOrder> elementoOrders)
        {
            try
            {
                ContSeccionCtrl.UpdatePosiciones(elementoOrders);
                return Ok("Posiciones cambiadas");
            }
            catch (SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ContSeccionCtrl.Terminar();
                ContSeccionCtrl = null;
            }
        }
    }
}
