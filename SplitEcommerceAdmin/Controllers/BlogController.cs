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
    public class BlogController : Controller
    {
        private BlogCtrl BlogCtrl;

        public BlogController(IConfiguration configuration)
        {
            BlogCtrl = new BlogCtrl(new Splittel(configuration));
        }
        public IActionResult Index(string Mode)
        {
            try
            {
                var result = BlogCtrl.Getblogs();
                ViewData["Mode"] = string.IsNullOrEmpty(Mode) ? "List" : Mode;
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                BlogCtrl.Terminar();
                BlogCtrl = null;
            }
        }

        public IActionResult Details(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = BlogCtrl.GetBlog(Int32.Parse(idDecripted));
                if(result is null)
                {
                    return NotFound("Blog no encontrado");
                }
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                BlogCtrl.Terminar();
                BlogCtrl = null;
            }
        }

        public IActionResult Edit(string id)
        {
            try
            {
                string idDecripted = EncryptData.Decrypt(id);
                var result = BlogCtrl.GetBlog(Int32.Parse(idDecripted));
                if (result is null)
                {
                    return NotFound("Blog no encontrado");
                }
                return View(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return NotFound(ex.Message);
            }
            finally
            {
                BlogCtrl.Terminar();
                BlogCtrl = null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(blog);
                }
                BlogCtrl.Update(blog);
                ViewData["MessageSuccess"] = $"El blog'{blog.Titulo}' ha sido actualizada, fecha de actualizacion: {DateTime.Now.ToString("yyyy-MM-dd hh:mm")}";
                return View(BlogCtrl.GetBlog(blog.IdBlog));
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                if(ex.ErrorCode == 10)
                {
                    ModelState.AddModelError(ex.IdAux, ex.Message);
                }
                else
                {
                    ModelState.AddModelError("", ex.Message);
                }
                
                return View(blog);
            }
            finally
            {
                BlogCtrl.Terminar();
                BlogCtrl = null;
            }
        }

        public IActionResult Create()
        {
            BlogCtrl.Terminar();
            BlogCtrl = null;
            return View();
        }
    }
}
