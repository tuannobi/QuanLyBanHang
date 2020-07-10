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
    public class PhanLoaiController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public PhanLoaiController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: PhanLoai
        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var quanLyBanHangDbContext = _context.PhanLoai.Include(p => p.NhomLoaiNavigation);
            return View(await quanLyBanHangDbContext.ToListAsync());
        }

        // GET: PhanLoai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            if (id == null)
            {
                return NotFound();
            }

            var phanLoai = await _context.PhanLoai
                .Include(p => p.NhomLoaiNavigation)
                .FirstOrDefaultAsync(m => m.PhanLoaiId == id);
            if (phanLoai == null)
            {
                return NotFound();
            }

            return View(phanLoai);
        }

        // GET: PhanLoai/Create
        public IActionResult Create()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            ViewData["NhomLoai"] = new SelectList(_context.PhanLoai, "PhanLoaiId", "PhanLoaiId");
            return View();
        }

        // POST: PhanLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhanLoaiId,Loai,MoTa,NhomLoai")] PhanLoai phanLoai)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            if (ModelState.IsValid)
            {
                _context.Add(phanLoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhomLoai"] = new SelectList(_context.PhanLoai, "PhanLoaiId", "PhanLoaiId", phanLoai.NhomLoai);
            return View(phanLoai);
        }

        // GET: PhanLoai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            if (id == null)
            {
                return NotFound();
            }

            var phanLoai = await _context.PhanLoai.FindAsync(id);
            if (phanLoai == null)
            {
                return NotFound();
            }
            ViewData["NhomLoai"] = new SelectList(_context.PhanLoai, "PhanLoaiId", "PhanLoaiId", phanLoai.NhomLoai);
            return View(phanLoai);
        }

        // POST: PhanLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhanLoaiId,Loai,MoTa,NhomLoai")] PhanLoai phanLoai)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");

            if (id != phanLoai.PhanLoaiId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phanLoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhanLoaiExists(phanLoai.PhanLoaiId))
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
            ViewData["NhomLoai"] = new SelectList(_context.PhanLoai, "PhanLoaiId", "PhanLoaiId", phanLoai.NhomLoai);
            return View(phanLoai);
        }

        // GET: PhanLoai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");

            if (id == null)
            {
                return NotFound();
            }

            var phanLoai = await _context.PhanLoai
                .Include(p => p.NhomLoaiNavigation)
                .FirstOrDefaultAsync(m => m.PhanLoaiId == id);
            if (phanLoai == null)
            {
                return NotFound();
            }

            return View(phanLoai);
        }

        // POST: PhanLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanLoai = await _context.PhanLoai.FindAsync(id);
            _context.PhanLoai.Remove(phanLoai);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhanLoaiExists(int id)
        {
            return _context.PhanLoai.Any(e => e.PhanLoaiId == id);
        }
    }
}
