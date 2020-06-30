using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Dynamic;



namespace QuanLyBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public SanPhamController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: SanPham
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPham.ToListAsync());
        }

        
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var CategoryQuery = from d in _context.PhanLoai
                                where d.NhomLoai != null && d.NhomLoai > 1 && d.NhomLoai < 5
                                orderby d.PhanLoaiId
                                select d;
            ViewBag.PhanLoaiId = new SelectList(CategoryQuery.AsNoTracking(), "PhanLoaiId", "Loai", selectedCategory);
        }

        public IActionResult Create()
        {
            PopulateCategoryDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenSanPham,GiaBanLe,Kho,ThuongHieu,TrangThai,PhanLoaiId,MoTa")] SanPham sanPham, IFormFile[] files)
        {
            var hinhanh = "";
            foreach (var photo in files)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/client/images/product", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream);
                hinhanh = hinhanh + photo.FileName + ",";
                //Console.WriteLine(hinhanh);
            }
            hinhanh = hinhanh.Remove(hinhanh.Length -1, 1);
            sanPham.HinhAnh = hinhanh;
            _context.Add(sanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var sp = await _context.SanPham
                .FirstOrDefaultAsync(m => m.SanPhamId == id);
            var hinhAnh = sp.HinhAnh.Split(',');
            ViewBag.SanPham = sp;
            ViewBag.HinhAnh = hinhAnh;
            PopulateCategoryDropDownList(sp.PhanLoaiId);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("SanPhamId,TenSanPham,GiaBanLe,Kho,ThuongHieu,TrangThai,PhanLoaiId,MoTa,HinhAnh")] SanPham sp, IFormFile[] files)
        {
            var hinhanh = "";
            if (files.Length > 0)
            {
                foreach (var photo in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/client/images/product", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    hinhanh = hinhanh + photo.FileName + ",";   
                }
                hinhanh = hinhanh.Remove(hinhanh.Length - 1, 1);
                sp.HinhAnh = hinhanh;
            }
            Console.WriteLine(hinhanh);
            _context.Update(sp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: SanPham/Delete/5
        [HttpPost]
        public string Delete(int id)
        {
            var sp = _context.SanPham.Find(id);
            _context.SanPham.Remove(sp);
            _context.SaveChanges();
            string message = "Xóa sản phẩm thành công";
            return message;
        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var sp = _context.SanPham.Find(id);
                _context.SanPham.Remove(sp);
            }
            _context.SaveChanges();
            return Json("All the selected producted deleted successfully!");
        }


    }
}
