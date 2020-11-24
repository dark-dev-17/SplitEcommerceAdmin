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

        [HttpPost]
        public ActionResult UpdateFile(string Codigo, IFormFile FormFile, ProductoTipoFile Tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    return BadRequest();
                }
                string idDecripted = EncryptData.Decrypt(Codigo);
                string result = ProductoCtrl.UpdateFile(idDecripted, FormFile, Tipo);
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
                ViewData["ImgsProducto"] = ProductoCtrl.GetFileProducto(idDecripted);
                ViewData["ImgsDescripcion"] = ProductoCtrl.GetFileProducto(idDecripted,ProductoTipoFile.Descripcion);
                ViewData["ImgsInfoAdicional"] = ProductoCtrl.GetFileProducto(idDecripted,ProductoTipoFile.InfoAdicional);
                ViewData["ImgsMiniatura"] = ProductoCtrl.GetFileProducto(idDecripted,ProductoTipoFile.Miniatura);
                ViewData["FichaTecnica"] = ProductoCtrl.GetFichaTecnica(result.Codigo, result.IdInfo_tecnica);
                ViewData["DescricionCompartida"] = ProductoCtrl.GetDescripcionCompartida(result.Codigo, result.IdDesclarga);
                ViewData["Categoria"] = ProductoCtrl.GetCategoria(result.Codigo, result.IdCategoria);
                ViewData["SubCategoria"] = ProductoCtrl.GetSubCategoria(result.Codigo, result.IdSubcategoria);
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

       
    }
}
