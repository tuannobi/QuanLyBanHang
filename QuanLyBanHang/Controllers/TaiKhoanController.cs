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
    public class TaiKhoanController : Controller
    {


        private readonly QuanLyBanHangDbContext _context;

        public TaiKhoanController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: TaiKhoan
        public async Task<IActionResult> Index()
        {
            var quanLyBanHangDbContext = _context.TaiKhoan.Include(t => t.VaiTro);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        //Get:TaiKhoan/Admin-NhanVien
        public IActionResult GetNhanVien(string id)
        {
            List<TaiKhoan> taikhoan = _context.TaiKhoan.ToList();
            List<VaiTro> vaitro = _context.VaiTro.ToList();
            List<NhanVien> nhanvien = _context.NhanVien.ToList();

            var taiKhoan_nhanvien = (from h in taikhoan
                                     join vtro in vaitro
                                     on h.VaiTroId equals vtro.VaiTroId into taikhoan_vaitro
                                     from vtro in taikhoan_vaitro.ToList()
                                     join nv in nhanvien
                                     on h.TaiKhoanId equals nv.TaiKhoanId into taikhoan_nhanvien
                                     from nv in taikhoan_nhanvien.ToList()
                                     where h.VaiTroId == 2

                                     select new TaiKhoan_NhanVien_KhachHang_Admin_VaiTro
                                     {
                                         taiKhoan = h,
                                         vaiTro = vtro,
                                         nhanVien = nv
                                     });

            if(!String.IsNullOrEmpty(id))
            {
                taiKhoan_nhanvien = taiKhoan_nhanvien.Where(search => search.nhanVien.HoTen.Contains(id));
            }
            taiKhoan_nhanvien = taiKhoan_nhanvien.ToList();

            return View(taiKhoan_nhanvien);
        }

        //Get:TaiKhoan/KhachHang
        public IActionResult GetKhachHang(string SearchString)
        {
            List<TaiKhoan> taikhoan = _context.TaiKhoan.ToList();
            List<VaiTro> vaitro = _context.VaiTro.ToList();
            List<KhachHang> khachhang = _context.KhachHang.ToList();

            var quanlykhachhang = (from h in taikhoan
                                   join vtro in vaitro
                                   on h.VaiTroId equals vtro.VaiTroId into taikhoan_vaitro
                                   from vtro in taikhoan_vaitro.ToList()
                                   join kh in khachhang
                                   on h.TaiKhoanId equals kh.TaiKhoanId into taikhoan_khachhang
                                   from kh in taikhoan_khachhang.ToList()
                                   where h.VaiTroId == 3
                                   select new TaiKhoan_NhanVien_KhachHang_Admin_VaiTro
                                   {
                                       taiKhoan = h,
                                       vaiTro = vtro,
                                       khachhang = kh
                                   });
            if (!String.IsNullOrEmpty(SearchString))
            {
               
                quanlykhachhang = quanlykhachhang.Where(search => search.khachhang.HoTen.ToLower().Contains(SearchString.ToLower()));
            }
            quanlykhachhang = quanlykhachhang.ToList();
            return View(quanlykhachhang);
        }
        //Get: TaiKhoan/Admin
        public IActionResult GetAdmin(string SearchString)
        {
            
            List<TaiKhoan> taikhoan = _context.TaiKhoan.ToList();
            List<VaiTro> vaitro = _context.VaiTro.ToList();
            List<Admin> admin = _context.Admin.ToList();

            var quanlyadmin = (from h in taikhoan
                                   join vtro in vaitro
                                   on h.VaiTroId equals vtro.VaiTroId into taikhoan_vaitro
                                   from vtro in taikhoan_vaitro.ToList()
                                   join ad in admin
                                   on h.TaiKhoanId equals ad.TaiKhoanId into taikhoan_admin
                                   from ad in taikhoan_admin.ToList()
                                   where h.VaiTroId == 1
                                   select new TaiKhoan_NhanVien_KhachHang_Admin_VaiTro
                                   {
                                       taiKhoan = h,
                                       vaiTro = vtro,
                                       admin = ad
                                   });
            if (!String.IsNullOrEmpty(SearchString))
            {
                quanlyadmin = quanlyadmin.Where(search => search.admin.HoTen.ToLower().Contains(SearchString.ToLower()));
                quanlyadmin = quanlyadmin.ToList();
            }
            return View(quanlyadmin);
        }

    
       


        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> EditAdmin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, [Bind("TaiKhoanId,Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taiKhoan.NgayTao = DateTime.Now;
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetAdmin));
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }
        //nhan vien

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> EditNhanVien(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNhanVien(int id, [Bind("TaiKhoanId,Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taiKhoan.NgayTao = DateTime.Now;
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetNhanVien));
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }


        //Khach hang

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> EditKhachHang(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditKhachHang(int id, [Bind("TaiKhoanId,Username,Password,NgayTao,VaiTroId")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TaiKhoanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taiKhoan.NgayTao = DateTime.Now;
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TaiKhoanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetKhachHang));
            }
            ViewData["VaiTroId"] = new SelectList(_context.VaiTro, "VaiTroId", "VaiTroId", taiKhoan.VaiTroId);
            return View(taiKhoan);
        }



        // GET: TaiKhoan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoan
                .Include(t => t.VaiTro)
                .FirstOrDefaultAsync(m => m.TaiKhoanId == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taiKhoan = await _context.TaiKhoan.FindAsync(id);
            _context.TaiKhoan.Remove(taiKhoan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(int id)
        {
            return _context.TaiKhoan.Any(e => e.TaiKhoanId == id);
        }
    }
}
