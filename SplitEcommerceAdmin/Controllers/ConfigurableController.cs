using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;

namespace SplitEcommerceAdmin.Controllers
{
    public class ConfigurableController : Controller
    {
        private ConfigurableCtrl ConfigurableCtrl;

        public ConfigurableController(IConfiguration configuration)
        {
            ConfigurableCtrl = new ConfigurableCtrl(new Splittel(configuration));
        }
        public IActionResult Index()
        {
            try
            {
                var result = ConfigurableCtrl.GetConfigurables();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ConfigurableCtrl.Terminar();
                ConfigurableCtrl = null;
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ConfigurableCtrl.GetConfigurable(idDecripted);
                if (result is null)
                {
                    return NotFound("No encontrado");
                }
                ViewData["Categoria"] = new CategoriaCtrl(ConfigurableCtrl.Splittel).GetCategorias(result.IdCategoria);
                ViewData["SubCategoria"] = new SubCategoriaCtrl(ConfigurableCtrl.Splittel).GetSubCategoria(result.IdSubCategoria);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ConfigurableCtrl.Terminar();
                ConfigurableCtrl = null;
            }
        }

        public IActionResult Edit(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ConfigurableCtrl.GetConfigurable(idDecripted);
                if (result is null)
                {
                    return NotFound("No encontrado");
                }
                ViewData["Categorias"] = new SelectList(new CategoriaCtrl(ConfigurableCtrl.Splittel).GetCategorias(),"Codigo", "DescripcionFamilia", result.IdCategoria);
                ViewData["SubCategorias"] = new SelectList(new SubCategoriaCtrl(ConfigurableCtrl.Splittel).GetSubCategorias(), "Codigo", "Nombre", result.IdSubCategoria);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ConfigurableCtrl.Terminar();
                ConfigurableCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Configurable Configurable)
        {
            ViewData["Categorias"] = new SelectList(new CategoriaCtrl(ConfigurableCtrl.Splittel).GetCategorias(), "Codigo", "DescripcionFamilia", Configurable.IdCategoria);
            ViewData["SubCategorias"] = new SelectList(new SubCategoriaCtrl(ConfigurableCtrl.Splittel).GetSubCategorias(), "Codigo", "Nombre", Configurable.IdSubCategoria);

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Configurable);
                }

                ConfigurableCtrl.Update(Configurable);
                ViewData["MessageSuccess"] = $"El configurable '{Configurable.Codigo}' ha sido actualizada, fecha de actualizacion: {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";
                return View(Configurable);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(Configurable);
            }
            finally
            {
                ConfigurableCtrl.Terminar();
                ConfigurableCtrl = null;
            }
        }
    }
}
