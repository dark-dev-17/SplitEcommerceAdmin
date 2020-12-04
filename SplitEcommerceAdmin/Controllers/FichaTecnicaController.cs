using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SplitAdminEcomerce;
using SplitAdminEcomerce.Controllers;

namespace SplitEcommerceAdmin.Controllers
{
    public class FichaTecnicaController : Controller
    {
        private FichaTecnicaCtrl FichaTecnicaCtrl;

        public FichaTecnicaController(IConfiguration configuration)
        {
            FichaTecnicaCtrl = new FichaTecnicaCtrl(new Splittel(configuration));
        }

        [HttpPost]
        public IActionResult GetList(string Folder)
        {
            try
            {
                var result = FichaTecnicaCtrl.ListDirectory(Folder);
                return Ok(result);
            }
            catch (SplitAdminEcomerce.Exceptions.SplitException ex)
            {
                return BadRequest(ex.Message);
            }
            finally
            {
                FichaTecnicaCtrl.Terminar();
                FichaTecnicaCtrl = null;
            }
        }
    }
}
