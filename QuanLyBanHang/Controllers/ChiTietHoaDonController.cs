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
    public class ChiTietHoaDonController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public ChiTietHoaDonController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: ChiTietHoaDon
        public async Task<IActionResult> Index()
        {
            var quanLyBanHangDbContext = _context.ChiTietHoaDon.Include(c => c.HoaDon).Include(c => c.SanPham);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: ChiTietHoaDon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon
                .Include(c => c.HoaDon)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Create
        public IActionResult Create()
        {
            ViewData["HoaDonId"] = new SelectList(_context.HoaDon, "HoaDonId", "HoaDonId");
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "SanPhamId", "SanPhamId");
            return View();
        }

        // POST: ChiTietHoaDon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HoaDonId,SanPhamId,SoLuong,TienKhuyenMai,TongTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietHoaDon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoaDonId"] = new SelectList(_context.HoaDon, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "SanPhamId", "SanPhamId", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon.FindAsync(id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }
            ViewData["HoaDonId"] = new SelectList(_context.HoaDon, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "SanPhamId", "SanPhamId", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HoaDonId,SanPhamId,SoLuong,TienKhuyenMai,TongTien")] ChiTietHoaDon chiTietHoaDon)
        {
            if (id != chiTietHoaDon.HoaDonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietHoaDon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietHoaDonExists(chiTietHoaDon.HoaDonId))
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
            ViewData["HoaDonId"] = new SelectList(_context.HoaDon, "HoaDonId", "HoaDonId", chiTietHoaDon.HoaDonId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPham, "SanPhamId", "SanPhamId", chiTietHoaDon.SanPhamId);
            return View(chiTietHoaDon);
        }

        // GET: ChiTietHoaDon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietHoaDon = await _context.ChiTietHoaDon
                .Include(c => c.HoaDon)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.HoaDonId == id);
            if (chiTietHoaDon == null)
            {
                return NotFound();
            }

            return View(chiTietHoaDon);
        }

        // POST: ChiTietHoaDon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chiTietHoaDon = await _context.ChiTietHoaDon.FindAsync(id);
            _context.ChiTietHoaDon.Remove(chiTietHoaDon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietHoaDonExists(int id)
        {
            return _context.ChiTietHoaDon.Any(e => e.HoaDonId == id);
        }
    }
}
