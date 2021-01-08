using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;
using SplitAdminEcomerce.Exceptions;
using SplitAdminEcomerce.Tools;
using SplitEcommerceAdmin.Models;

namespace SplitEcommerceAdmin.Controllers
{
    public class ConsultorTecnicoController : Controller
    {
        private ConsultorTecCtrl ConsultorTecCtrl;

        public ConsultorTecnicoController(IConfiguration configuration)
        {
            ConsultorTecCtrl = new ConsultorTecCtrl(new Splittel(configuration));
        }
        [HttpPost]
        public IActionResult AddConsultores([FromBody]ConsultConsult consultConsult)
        {
            try
            {
                if(consultConsult is null)
                {
                    throw new SplitException { Category = TypeException.Info, Description = $"Error al asignar consultores, por favor intenta de nuevo", ErrorCode = 100 };
                }
                ConsultorTecCtrl.AddConsultores(consultConsult.Consultores, consultConsult.IdConsultorTecnico_);
                return Ok("Cambios guardados exitosamente");
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
        [HttpPost]
        public IActionResult DetailsRespuesta(int IdConsultorRespuestas)
        {
            try
            {
                var respues_re = ConsultorTecCtrl.GetRespuesta(IdConsultorRespuestas);
                return Ok(respues_re);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
        [HttpPost]
        public IActionResult Crear(IFormFile Archivo, string Respuesta_, int IdConsultor_, int IdConsultorTecnico_, string Source)
        {
            try
            {
                ConsultorTecCtrl.Create(Archivo, Respuesta_, IdConsultor_, IdConsultorTecnico_, Source);
                return Ok("Respuesta guardada exitosamente");
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
        [HttpPost]
        public IActionResult EditarRespuesta(string Respuesta_, int IdConsultor_, int IdConsultorTecnico_, int IdConsultorRespuestas)
        {
            try
            {
                ConsultorTecCtrl.Edit(Respuesta_, IdConsultor_, IdConsultorTecnico_, IdConsultorRespuestas);
                return Ok("Respuesta guardada exitosamente");
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }

        public IActionResult Asignar()
        {
            try
            {
                var result = ConsultorTecCtrl.GetConsultorTecnicos();
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
        [HttpPost]
        public IActionResult Desactivar(string id, bool Ative)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);

                ConsultorTecCtrl.DesactivarPregunta(Int32.Parse(idDecripted), Ative);

                return Ok("Cambios guardados");
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = ConsultorTecCtrl.GetConsultorTecnico(Int32.Parse(idDecripted));
                if (result is null)
                {
                    return NotFound("Solicitud asesoría técnica no encontrada");
                }
                ViewData["Respuestas"] = ConsultorTecCtrl.GetRespuestas(Int32.Parse(idDecripted));
                ViewData["Consultores"] = ConsultorTecCtrl.GetConsultores();
                ViewData["ConsultorConsultor"] = ConsultorTecCtrl.GetConsultoresPre(Int32.Parse(idDecripted));
                ViewData["Sitio"] = ConsultorTecCtrl.Splittel.FtpServ.Site;
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                ConsultorTecCtrl.Terminar();
                ConsultorTecCtrl = null;
            }
        }
    }
}
