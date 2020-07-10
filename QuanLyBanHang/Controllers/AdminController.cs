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
    
    [ServiceFilter(typeof(AdminFilter))]
    public class AdminController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public AdminController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var quanLyBanHangDbContext = _context.Admin.Include(a => a.TaiKhoan);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .Include(a => a.TaiKhoan)
                .FirstOrDefaultAsync(m => m.Adminid == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            ViewData["TaiKhoanId"] = new SelectList(_context.TaiKhoan, "TaiKhoanId", "TaiKhoanId");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("HoTen,NgaySinh,Email,Sdt,DiaChi,TaiKhoanId")] Admin admin, [Bind("Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {

            taiKhoan.VaiTroId = 1;
            _context.Add(taiKhoan);
            _context.SaveChanges();

            var tkhoan = _context.TaiKhoan
                        .FirstOrDefault(tk => tk.Username == taiKhoan.Username);
            admin.TaiKhoanId = tkhoan.TaiKhoanId;
            _context.Add(admin);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var ad = await _context.Admin
                .FirstOrDefaultAsync(m => m.Adminid == id);

            var taikhoan = _context.TaiKhoan.Find(ad.TaiKhoanId);
            ViewBag.Admin = ad;
            ViewBag.TaiKhoan = taikhoan;

            if (ad.NgaySinh != null)
            {
                DateTime x = (DateTime)ad.NgaySinh;
                string formattedDate = x.ToString("yyyy-MM-dd HH:mm:ss");
                formattedDate = formattedDate.Replace(" ", "T");
                ViewBag.NgaySinh = formattedDate;
            }
            else
            {
                ViewBag.NgaySinh = ad.NgaySinh;
            }


            return View(ad);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Adminid,HoTen,NgaySinh,Email,Sdt,DiaChi")] Admin admin, [Bind("TaiKhoanId,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {


            var ad = (from AD in _context.Admin
                      where AD.Adminid == admin.Adminid
                      select AD.TaiKhoanId).FirstOrDefault();
            admin.TaiKhoanId = ad;
            _context.Update(admin);
            _context.SaveChanges();
            taiKhoan.VaiTroId = 1;
            taiKhoan.TaiKhoanId = ad;
            _context.Update(taiKhoan);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        // POST: SanPham/Delete/5
        [HttpPost]

        public string Delete(int id)
        {

            var admin = _context.Admin.Find(id);
            var taikhoan = _context.TaiKhoan.Find(admin.TaiKhoanId);
            _context.Admin.Remove(admin);
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
                var ad = _context.Admin.Find(id);
                var tk = _context.TaiKhoan.Find(ad.TaiKhoanId);
                _context.Admin.Remove(ad);
                _context.SaveChanges();
                _context.TaiKhoan.Remove(tk);
                _context.SaveChanges();
            }

            return Json("All the selected producted deleted successfully!");
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.Adminid == id);
        }
    }
}
