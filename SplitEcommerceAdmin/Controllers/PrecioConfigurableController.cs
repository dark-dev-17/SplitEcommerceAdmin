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
    public class PrecioConfigurableController : Controller
    {
        private PrecioConfCtrl PrecioConfCtrl;

        public PrecioConfigurableController(IConfiguration configuration)
        {
            PrecioConfCtrl = new PrecioConfCtrl(new Splittel(configuration));
        }
        public IActionResult Index()
        {
            return View();
        }
        #region Cable de servicio
        [HttpPost]
        public IActionResult CableServList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioCableServs();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CableServEdit(PrecioCableServ precioCableServ)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(precioCableServ);
                }
                PrecioConfCtrl.EditPrecioCableServs(precioCableServ);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(precioCableServ);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(precioCableServ);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult CableServEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioCableServ(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region Distribuidores precargados
        [HttpPost]
        public IActionResult DistriPrecaList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioDistriPrecas();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DistriPrecaEdit(PrecioDistriPreca PrecioDistriPreca)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioDistriPreca);
                }
                PrecioConfCtrl.EditPrecioDistriPreca(PrecioDistriPreca);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioDistriPreca);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioDistriPreca);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult DistriPrecaEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioDistriPreca(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region Distribuidores preconectorizados
        [HttpPost]
        public IActionResult DistriPreconList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioDistriPrecons();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DistriPreconEdit(PrecioDistriPrecon PrecioDistriPrecon)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioDistriPrecon);
                }
                PrecioConfCtrl.EditPrecioDistriPrecon(PrecioDistriPrecon);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioDistriPrecon);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioDistriPrecon);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult DistriPreconEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioDistriPrecon(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region Jumper cable
        [HttpPost]
        public IActionResult JumperCableList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioJumperCables();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JumperCableEdit(PrecioJumperCable PrecioJumperCable)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioJumperCable);
                }
                PrecioConfCtrl.EditPrecioJumperCable(PrecioJumperCable);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioJumperCable);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioJumperCable);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult JumperCableEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioJumperCable(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region Jumper conector
        [HttpPost]
        public IActionResult JumperConectList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioJumperConects();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JumperConectEdit(PrecioJumperConect PrecioJumperConect)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioJumperConect);
                }
                PrecioConfCtrl.EditPrecioJumperConect(PrecioJumperConect);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioJumperConect);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioJumperConect);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult JumperConectEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioJumperConect(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region MPO
        [HttpPost]
        public IActionResult MPOList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioMPOs();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MPOEdit(PrecioMPO PrecioMPO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioMPO);
                }
                PrecioConfCtrl.EditPrecioMPO(PrecioMPO);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioMPO);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioMPO);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult MPOEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioMPO(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region PatchCord
        [HttpPost]
        public IActionResult PatchCordList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioPatchCords();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PatchCordEdit(PrecioPatchCord PrecioPatchCord)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioPatchCord);
                }
                PrecioConfCtrl.EditPrecioPatchCord(PrecioPatchCord);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioPatchCord);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioPatchCord);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult PatchCordEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioPatchCord(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion

        #region Pigtail
        [HttpPost]
        public IActionResult PigtailList()
        {
            try
            {
                var result = PrecioConfCtrl.GetPrecioPigtails();
                return PartialView(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PigtailEdit(PrecioPigtail PrecioPigtail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return PartialView(PrecioPigtail);
                }
                PrecioConfCtrl.EditPrecioPigtail(PrecioPigtail);
                ViewData["MessageSuccess"] = $"Se han guardado los cambios exitosamente";
                return PartialView(PrecioPigtail);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                ModelState.AddModelError(string.IsNullOrEmpty(ex.IdAux) ? "" : ex.IdAux, ex.Message);
                return PartialView(PrecioPigtail);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        [HttpGet]
        public IActionResult PigtailEdit(string id)
        {
            try
            {
                var res = PrecioConfCtrl.GetPrecioPigtail(Int32.Parse(EncryptData.Decrypt(id)));
                if (res is null)
                    return NotFound();
                return PartialView(res);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                PrecioConfCtrl.Terminar();
                PrecioConfCtrl = null;
            }
        }
        #endregion
    }
}
