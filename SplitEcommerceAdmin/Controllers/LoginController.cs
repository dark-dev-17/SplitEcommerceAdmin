using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SplitEcommerceAdmin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Start()
        {
            return View();
        }
    }
}
