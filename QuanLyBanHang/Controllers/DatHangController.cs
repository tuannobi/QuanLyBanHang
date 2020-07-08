using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class DatHangController : Controller
    {
        [ServiceFilter(typeof(LoginFilter))]
        public IActionResult Index()
        {
            return View();
        }
    }
}