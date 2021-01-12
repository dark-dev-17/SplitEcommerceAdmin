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
using SplitEcommerceAdmin.Models;

namespace SplitEcommerceAdmin.Controllers
{
    public class HomeSlideController : Controller
    {
        private HomeSlideCtrl HomeSlideCtrl;

        public HomeSlideController(IConfiguration configuration)
        {
            HomeSlideCtrl = new HomeSlideCtrl(new Splittel(configuration));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HomeSlide HomeSlide)
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            ViewData["GruposSap"] = new SelectList(HomeSlideCtrl.getGruposSap(), "GroupCode", "GroupName", HomeSlide.SapGrupo);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(HomeSlide);
                }
                HomeSlideCtrl.Crear(HomeSlide);
                ViewData["MessageSuccess"] = "Slide actualizada";
                return View(HomeSlide);
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(ex.NameObject is null ? "" : ex.NameObject, ex.Message);
                return View(HomeSlide);
            }
            finally
            {
                HomeSlideCtrl.Terminar();
                HomeSlideCtrl = null;
            }
        }
        public IActionResult Create()
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            ViewData["GruposSap"] = new SelectList(HomeSlideCtrl.getGruposSap(), "GroupCode", "GroupName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeSlide HomeSlide)
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            ViewData["GruposSap"] = new SelectList(HomeSlideCtrl.getGruposSap(), "GroupCode", "GroupName", HomeSlide.SapGrupo);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(HomeSlide);
                }
                HomeSlideCtrl.Editar(HomeSlide);
                ViewData["MessageSuccess"] = "Slide actualizado";
                return View(HomeSlideCtrl.GetHomeSlide(HomeSlide.IdHomeSlide));
            }
            catch (SplitException ex)
            {
                ModelState.AddModelError(ex.NameObject is null ? "" : ex.NameObject, ex.Message);
                return View(HomeSlide);
            }
            finally
            {
                HomeSlideCtrl.Terminar();
                HomeSlideCtrl = null;
            }
        }
        public IActionResult Edit(string id)
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = HomeSlideCtrl.GetHomeSlide(Int32.Parse(idDecripted));
                ViewData["GruposSap"] = new SelectList(HomeSlideCtrl.getGruposSap(), "GroupCode", "GroupName", result.SapGrupo);

                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                HomeSlideCtrl.Terminar();
                HomeSlideCtrl = null;
            }
        }
        public IActionResult Details(string id)
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = HomeSlideCtrl.GetHomeSlide(Int32.Parse(idDecripted));
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                HomeSlideCtrl.Terminar();
                HomeSlideCtrl = null;
            }
        }
        public IActionResult Index()
        {
            ViewData["Sitio"] = HomeSlideCtrl.Splittel.FtpServ.Site;
            try
            {
                var result = HomeSlideCtrl.GetHomeSlides();
                return View(result);
            }
            catch (SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                HomeSlideCtrl.Terminar();
                HomeSlideCtrl = null;
            }
        }

    }
}
