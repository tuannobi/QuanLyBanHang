using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public KhuyenMaiController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var ProductQuery = from d in _context.SanPham
                               select d;
            ViewBag.SanPhamId = new SelectList(ProductQuery.AsNoTracking(), "SanPhamId", "TenSanPham", selectedCategory);
        }
        public async Task<IActionResult> Index()
        {
            PopulateCategoryDropDownList();
            return View();
        }
        public async Task<IActionResult> Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }
    }
}
