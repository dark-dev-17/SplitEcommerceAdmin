using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;

namespace SplitEcommerceAdmin.Controllers
{
    public class DescripcionCompartidaController : Controller
    {
        private DescricionCompartidaCtrl DescricionCompartidaCtrl;

        public DescripcionCompartidaController(IConfiguration configuration)
        {
            DescricionCompartidaCtrl = new DescricionCompartidaCtrl(new Splittel(configuration));
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = DescricionCompartidaCtrl.GetDescripcion(idDecripted);
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DescricionCompartidaCtrl.Terminar();
                DescricionCompartidaCtrl = null;
            }
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = DescricionCompartidaCtrl.GetDescripcion(idDecripted);
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DescricionCompartidaCtrl.Terminar();
                DescricionCompartidaCtrl = null;
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(DescripcionCompartida descripcionCompartida)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(descripcionCompartida);
                }
                DescricionCompartidaCtrl.Update(descripcionCompartida);
                ViewData["Message"] = "Información guardada";
                return PartialView(descripcionCompartida);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError("",ex.Message);
                return PartialView(descripcionCompartida);
            }
            finally
            {
                DescricionCompartidaCtrl.Terminar();
                DescricionCompartidaCtrl = null;
            }
        }

        [HttpPost]
        public IActionResult Get(string Patron)
        {
            try
            {
                var result = DescricionCompartidaCtrl.Buscardor(Patron);
                return Ok(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                DescricionCompartidaCtrl.Terminar();
                DescricionCompartidaCtrl = null;
            }
        }
        public IActionResult Descripcion()
        {
            return View();
        }
    }
}
