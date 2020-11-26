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
        public ActionResult CarbirDescripcion(string Codigo, string clave)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(Codigo);
                ProductoCtrl.CambioDescripcion(idDecripted, clave);
                var result = ProductoCtrl.GetDescripcionCompartida(idDecripted, clave);
                return PartialView("../DescripcionCompartida/Edit", result);
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
                return Ok(new { ruta = result, name = FormFile.FileName });
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
        [HttpPost]
        public ActionResult DeeteFile(string Codigo, string FileName, ProductoTipoFile Tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    return BadRequest();
                }
                string idDecripted = EncryptData.Decrypt(Codigo);
                ProductoCtrl.DeleteFile(idDecripted, FileName, Tipo);
                return Ok("Archivo eliminado");
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

        [HttpPost]
        public ActionResult RenameFile(string Codigo, string FileOld, string NameNew, ProductoTipoFile Tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    return BadRequest();
                }
                string idDecripted = EncryptData.Decrypt(Codigo);
                ProductoCtrl.RenameFile(idDecripted, FileOld, NameNew,Tipo);
                return Ok("Archivo renombrado");
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
        [HttpPost]
        public ActionResult Activar(string Codigo, bool Active)
        {
            try
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    return BadRequest();
                }
                string idDecripted = EncryptData.Decrypt(Codigo);
                ProductoCtrl.Activar(idDecripted, Active);
                return Ok("Cambios guardados");
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


        public ActionResult ImgFiles(string Codigo, ProductoTipoFile Tipo)
        {
            try
            {
                if (string.IsNullOrEmpty(Codigo))
                {
                    return BadRequest("Por favor selecciona un producto");
                }
                string idDecripted = EncryptData.Decrypt(Codigo);
                var result = ProductoCtrl.GetFileProducto(idDecripted, Tipo);
                return PartialView(result);
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
       
    }
}
