using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public NhaCungCapController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            return View(await _context.NhaCungCap.ToListAsync());
        }

        public IActionResult Create()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhaCungCapId,TenNhaCungCap,DiaChi,Email,Sdt")] NhaCungCap nhaCungCap)
        {
             _context.Add(nhaCungCap);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: NhaCungCap/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var nhaCungCap = await _context.NhaCungCap.FirstOrDefaultAsync(m => m.NhaCungCapId == id);
            return View(nhaCungCap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("NhaCungCapId,TenNhaCungCap,DiaChi,Email,Sdt")] NhaCungCap nhaCungCap)
        {
            _context.Update(nhaCungCap);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string Delete(int id)
        {
            var ncc = _context.NhaCungCap.Find(id);
            _context.NhaCungCap.Remove(ncc);
            _context.SaveChanges();
            string message = "Xóa nhà cung cấp thành công";
            return message;
        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var ncc = _context.NhaCungCap.Find(id);
                _context.NhaCungCap.Remove(ncc);
            }
            _context.SaveChanges();
            return Json("Tất cả nhà cung cấp được chọn đã xóa thành công");
        }
    }
}
