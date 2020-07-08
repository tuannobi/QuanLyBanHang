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
    public class NhanVienController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public NhanVienController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index()
        {

            var quanLyBanHangDbContext = _context.NhanVien.Include(n => n.TaiKhoan);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanVien
                .Include(n => n.TaiKhoan)
                .FirstOrDefaultAsync(m => m.NhanVienId == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoan, "TaiKhoanId", "TaiKhoanId");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,NgaySinh,Email,Sdt,DiaChi,TaiKhoanId")] NhanVien nhanVien, [Bind("Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {

            taiKhoan.VaiTroId = 2;
            _context.Add(taiKhoan);
            _context.SaveChanges();

            var tkhoan =  _context.TaiKhoan
                        .FirstOrDefault(tk => tk.Username == taiKhoan.Username);
            nhanVien.TaiKhoanId = tkhoan.TaiKhoanId;
            _context.Add(nhanVien);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var nv = await _context.NhanVien
           .FirstOrDefaultAsync(m => m.NhanVienId == id);

            var taikhoan = _context.TaiKhoan.Find(nv.TaiKhoanId);
            ViewBag.NhanVien = nv;
            ViewBag.TaiKhoan = taikhoan;
            if (nv.NgaySinh != null)
            {
                DateTime x = (DateTime)nv.NgaySinh;
                string formattedDate = x.ToString("yyyy-MM-dd HH:mm:ss");
                formattedDate = formattedDate.Replace(" ", "T");
                ViewBag.NgaySinh = formattedDate;
            }
            else
            {
                ViewBag.NgaySinh = nv.NgaySinh;
            }
            return View(nv);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("NhanVienId,HoTen,NgaySinh,Email,Sdt,DiaChi")] NhanVien nhanVien, [Bind("TaiKhoanId,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {


            var nv = (from NV in _context.NhanVien
                      where NV.NhanVienId == nhanVien.NhanVienId
                      select NV.TaiKhoanId).FirstOrDefault();
            nhanVien.TaiKhoanId = nv;
            _context.Update(nhanVien);
            _context.SaveChanges();
            taiKhoan.VaiTroId = 2;
            taiKhoan.TaiKhoanId = nv;           
            _context.Update(taiKhoan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // POST: SanPham/Delete/5
        [HttpPost]

        public string Delete(int id)
        {

            var nhanVien = _context.NhanVien.Find(id);
            var taikhoan = _context.TaiKhoan.Find(nhanVien.TaiKhoanId);
            _context.NhanVien.Remove(nhanVien);
            _context.SaveChanges();
            _context.TaiKhoan.Remove(taikhoan);
            _context.SaveChanges();
            string message = "Xóa thành công";
            return message;
        }
        // POST: SanPham/Delete/5
        [HttpPost]
        public ActionResult DeleteSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var nv = _context.NhanVien.Find(id);
                var tk = _context.TaiKhoan.Find(nv.TaiKhoanId);
                _context.NhanVien.Remove(nv);
                _context.SaveChanges();
                _context.TaiKhoan.Remove(tk);
                _context.SaveChanges();
            }
            
            return Json("All the selected producted deleted successfully!");
        }
        private bool NhanVienExists(int id)
        {
            return _context.NhanVien.Any(e => e.NhanVienId == id);
        }

    }

}
