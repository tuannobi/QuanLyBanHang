using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public CategoryController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? id)
        {
            var productList = context.SanPham.Where(sp => sp.PhanLoaiId == id);
            ViewBag.productList = productList;
            return View();
        }
    }
}