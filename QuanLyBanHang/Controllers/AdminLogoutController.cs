using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class AdminLogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Remove("sessionUser");
            return Redirect("/Login");
        }
    }
}