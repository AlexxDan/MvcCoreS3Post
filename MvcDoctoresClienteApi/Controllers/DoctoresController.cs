using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDoctoresClienteApi.Controllers
{
    public class DoctoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DoctoresClienteAjax()
        {
            return View();
        }
    }
}
