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
    [ServiceFilter(typeof(AdminLoginFilter))]
    public class KhachHangController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public KhachHangController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: KhachHang
        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var quanLyBanHangDbContext = _context.KhachHang.Include(k => k.TaiKhoan);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: KhachHang/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHang
                .Include(k => k.TaiKhoan)
                .FirstOrDefaultAsync(m => m.KhachHangId == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: KhachHang/Create
        public IActionResult Create()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoan, "TaiKhoanId", "TaiKhoanId");
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,NgaySinh,Email,Sdt,DiaChi,TaiKhoanId")] KhachHang khachHang, [Bind("Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {

            taiKhoan.VaiTroId = 3;
            _context.Add(taiKhoan);
            _context.SaveChanges();
            var tkhoan = _context.TaiKhoan
                        .FirstOrDefault(tk => tk.Username == taiKhoan.Username);
            khachHang.TaiKhoanId = tkhoan.TaiKhoanId;
            _context.Add(khachHang);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");

            var kh = await _context.KhachHang
                .FirstOrDefaultAsync(m => m.KhachHangId == id);

            var taikhoan = _context.TaiKhoan.Find(kh.TaiKhoanId);
            ViewBag.KhachHang = kh;
            ViewBag.TaiKhoan = taikhoan;
            if (kh.NgaySinh != null)
            {
                DateTime x = (DateTime)kh.NgaySinh;
                string formattedDate = x.ToString("yyyy-MM-dd HH:mm:ss");
                formattedDate = formattedDate.Replace(" ", "T");
                ViewBag.NgaySinh = formattedDate;
            }
            else
            {
                ViewBag.NgaySinh = kh.NgaySinh;
            }
            return View(kh);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("KhachHangId,HoTen,NgaySinh,Email,Sdt,DiaChi")] KhachHang khachHang, [Bind("TaiKhoanId,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {
            var kh = (from KH in _context.KhachHang
                      where KH.KhachHangId == khachHang.KhachHangId
                      select KH.TaiKhoanId).FirstOrDefault();
            Console.WriteLine(kh);
            khachHang.TaiKhoanId = kh;            
            _context.Update(khachHang);
            _context.SaveChanges();
            taiKhoan.VaiTroId = 3;
            taiKhoan.TaiKhoanId = (int)kh;           
            _context.Update(taiKhoan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        // POST: SanPham/Delete/5
        [HttpPost]

        public string Delete(int id)
        {

            var khachHang = _context.KhachHang.Find(id);
            var taikhoan = _context.TaiKhoan.Find(khachHang.TaiKhoanId);
            _context.KhachHang.Remove(khachHang);
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
                var kh = _context.KhachHang.Find(id);
                var tk = _context.TaiKhoan.Find(kh.TaiKhoanId);
                _context.KhachHang.Remove(kh);
                _context.SaveChanges();
                _context.TaiKhoan.Remove(tk);
                _context.SaveChanges();
            }

            return Json("All the selected producted deleted successfully!");
        }
        private bool KhachHangExists(int id)
        {
            return _context.KhachHang.Any(e => e.KhachHangId == id);
        }
    }
}
