using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Tools;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Exceptions;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce.Models;

namespace SplitEcommerceAdmin.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaCtrl CategoriaCtrl;

        public CategoriaController(IConfiguration configuration)
        {
            CategoriaCtrl = new CategoriaCtrl(new Splittel(configuration));
        }

        public IActionResult Index()
        {
            try
            {
                var result = CategoriaCtrl.GetCategorias();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                CategoriaCtrl.Terminar();
                CategoriaCtrl = null;
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = CategoriaCtrl.GetCategorias(idDecripted);
                if (result is null)
                {
                    return NotFound("No encontrado");
                }
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                CategoriaCtrl.Terminar();
                CategoriaCtrl = null;
            }
        }

        public IActionResult Edit(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = CategoriaCtrl.GetCategorias(idDecripted);
                if(result is null)
                {
                    return NotFound("No encontrado");
                }
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                CategoriaCtrl.Terminar();
                CategoriaCtrl = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria Categoria)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Categoria);
                }
                CategoriaCtrl.Update(Categoria);
                ViewData["MessageSuccess"] = $"La categoria '{Categoria.Codigo}' ha sido actualizada, fecha de actualizacion: {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";
                return View(Categoria);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(Categoria);
            }
            finally
            {
                CategoriaCtrl.Terminar();
                CategoriaCtrl = null;
            }
        }
    }
}
