using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WMAgility.Data;

namespace WMAgility.Controllers
{
    public class BarChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BarChart()
        {
            return View();
        }
    }
}
