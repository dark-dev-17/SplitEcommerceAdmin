using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Tools;

namespace SplitEcommerceAdmin.Controllers
{
    public class ProductoController : Controller
    {
        private ProductoCtrl ProductoCtrl;

        public ProductoController(IConfiguration configuration)
        {
            ProductoCtrl = new ProductoCtrl(new Splittel(configuration));
        }
        [HttpPost]
        public ActionResult Buscador(string Codigo, string Columna)
        {
            try
            {
                var result = ProductoCtrl.Buscador(Codigo, Columna);
                return Ok(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ProductoCtrl.Terminar();
                ProductoCtrl = null;
            }
        }

        // GET: ProductoController
        public ActionResult Index()
        {
            try
            {
                var result = ProductoCtrl.Get();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ProductoCtrl.Terminar();
                ProductoCtrl = null;
            }
        }

        // GET: ProductoController/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return View();
                }
                string idDecripted = EncryptData.Decrypt(id);
                var result = ProductoCtrl.Get(idDecripted);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ProductoCtrl.Terminar();
                ProductoCtrl = null;
            }
        }

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ProductoCtrl.Get(idDecripted);
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ProductoCtrl.Terminar();
                ProductoCtrl = null;
            }
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
