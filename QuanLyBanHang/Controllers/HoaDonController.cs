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
    public class HoaDonController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public HoaDonController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public async Task<IActionResult> Index()
        {
            var quanLyBanHangDbContext = _context.HoaDon.Include(h => h.KhachHang).Include(h => h.NhanVien).Include(h => h.PhiShip);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: HoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.PhiShip)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // GET: HoaDon/Create
        public IActionResult Create()
        {
            ViewData["KhachHangId"] = new SelectList(_context.KhachHang, "KhachHangId", "KhachHangId");
            ViewData["NhanVienId"] = new SelectList(_context.NhanVien, "NhanVienId", "NhanVienId");
            ViewData["PhiShipId"] = new SelectList(_context.PhiShip, "PhiShipId", "PhiShipId");
            return View();
        }

        // POST: HoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoaDonId,PhuongThucThanhToan,SoNha,Quan,PhiShipId,KhachHangId,NhanVienId,TongTien,TongTienThanhToan,GhiChu,TrangThai,ThoiGianChoXuLy,ThoiGianDaXuLy")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHang, "KhachHangId", "KhachHangId", hoaDon.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanVien, "NhanVienId", "NhanVienId", hoaDon.NhanVienId);
            ViewData["PhiShipId"] = new SelectList(_context.PhiShip, "PhiShipId", "PhiShipId", hoaDon.PhiShipId);
            return View(hoaDon);
        }

        // GET: HoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHang, "KhachHangId", "KhachHangId", hoaDon.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanVien, "NhanVienId", "NhanVienId", hoaDon.NhanVienId);
            ViewData["PhiShipId"] = new SelectList(_context.PhiShip, "PhiShipId", "PhiShipId", hoaDon.PhiShipId);
            return View(hoaDon);
        }

        // POST: HoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoaDonId,PhuongThucThanhToan,SoNha,Quan,PhiShipId,KhachHangId,NhanVienId,TongTien,TongTienThanhToan,GhiChu,TrangThai,ThoiGianChoXuLy,ThoiGianDaXuLy")] HoaDon hoaDon)
        {
            if (id != hoaDon.HoaDonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoaDonExists(hoaDon.HoaDonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhachHangId"] = new SelectList(_context.KhachHang, "KhachHangId", "KhachHangId", hoaDon.KhachHangId);
            ViewData["NhanVienId"] = new SelectList(_context.NhanVien, "NhanVienId", "NhanVienId", hoaDon.NhanVienId);
            ViewData["PhiShipId"] = new SelectList(_context.PhiShip, "PhiShipId", "PhiShipId", hoaDon.PhiShipId);
            return View(hoaDon);
        }

        // GET: HoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoaDon = await _context.HoaDon
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.PhiShip)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        // POST: HoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            _context.HoaDon.Remove(hoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDon.Any(e => e.HoaDonId == id);
        }
    }
}
