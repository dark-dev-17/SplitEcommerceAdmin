using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Models;
using SplitAdminEcomerce.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SplitEcommerceAdmin.Controllers
{
    public class SubCategoriaController : Controller
    {
        private SubCategoriaCtrl SubCategoriaCtrl;
    

        public SubCategoriaController(IConfiguration configuration)
        {
            SubCategoriaCtrl = new SubCategoriaCtrl(new Splittel(configuration));
        }

        public IActionResult Index()
        {
            try
            {
                var result = SubCategoriaCtrl.GetSubCategorias();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                SubCategoriaCtrl.Terminar();
                SubCategoriaCtrl = null;
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = SubCategoriaCtrl.GetSubCategoria(idDecripted);
                if (result is null)
                {
                    return NotFound("No encontrado");
                }
                ViewData["Categoria"] = new CategoriaCtrl(SubCategoriaCtrl.Splittel).GetCategorias(result.IdFamilia);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                SubCategoriaCtrl.Terminar();
                SubCategoriaCtrl = null;
            }
        }

        public IActionResult Edit(string id)
        {
            SubCategoria subCategoria = null;
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                subCategoria = SubCategoriaCtrl.GetSubCategoria(idDecripted);
                if (subCategoria is null)
                {
                    return NotFound("No encontrado");
                }
                ViewData["ListCategorias"] = new SelectList(new CategoriaCtrl(SubCategoriaCtrl.Splittel).GetCategorias(), "Codigo", "DescripcionFamilia", subCategoria.IdFamilia);
                return View(subCategoria);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(subCategoria);
            }
            finally
            {
                SubCategoriaCtrl.Terminar();
                SubCategoriaCtrl = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SubCategoria SubCategoria)
        {
            ViewData["ListCategorias"] = new SelectList(new CategoriaCtrl(SubCategoriaCtrl.Splittel).GetCategorias(), "Codigo", "DescripcionFamilia", SubCategoria.IdFamilia);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(SubCategoria);
                }
                SubCategoriaCtrl.Update(SubCategoria);
                ViewData["MessageSuccess"] = $"La Sub-categoria '{SubCategoria.Codigo}' ha sido actualizada, fecha de actualizacion: {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";
                return View(SubCategoria);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(SubCategoria);
            }
            finally
            {
                SubCategoriaCtrl.Terminar();
                SubCategoriaCtrl = null;
            }
        }
    }
}
