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
    public class PhieuNhapController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public PhieuNhapController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: PhieuNhap
        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            var CategoryQuery = from d in _context.PhanLoai
                                where d.NhomLoai != null && d.NhomLoai > 1 && d.NhomLoai < 5
                                orderby d.PhanLoaiId
                                select d;
            ViewBag.PhanLoaiId = new SelectList(CategoryQuery.AsNoTracking(), "PhanLoaiId", "Loai", selectedCategory);
        }
        private void PopulateVendorDropDownList(object selectedCategory = null)
        {
            var VendorQuery = from d in _context.NhaCungCap
                                select d;
            ViewBag.NhaCungCapId = new SelectList(VendorQuery.AsNoTracking(), "NhaCungCapId", "TenNhaCungCap", selectedCategory);
        }
        public ActionResult GetProduct(int id)
        {
            var listProduct = from d in _context.SanPham
                              where d.PhanLoaiId == id
                              select d;
            var s = "<option value =''>Chọn sản phẩm</option>";
            foreach (var item in listProduct)
            {
                s += "<option value =" + item.SanPhamId + ">" + item.TenSanPham + "</option>";
            }
            return Json(s);
        }
        public ActionResult ThemPN(int mancc, DateTime ngnhap, List<SPNhap> listSP)
        {
            //List<SPKM> listSP = JsonConvert.DeserializeObject<List<SPKM>>(listKM); 
            Console.WriteLine(mancc);
            Console.WriteLine(ngnhap);
            foreach (var item in listSP)
            {
                Console.WriteLine(item.GIA);
                Console.WriteLine(item.SL);
            }    
            List<PhieuNhapSanPham> CTPN = new List<PhieuNhapSanPham>();
            foreach (var item in listSP)
            {
                var ctpn = new PhieuNhapSanPham();
                var tongtien = item.GIA * item.SL;
                ctpn.SanPhamId = item.ID;
                ctpn.GiaGoc = item.GIA;
                ctpn.SoLuong = item.SL;
                ctpn.TongTien = tongtien;
                CTPN.Add(ctpn);
            }
            var pn = new PhieuNhap();
            pn.NhaCungCapId = mancc;
            pn.NgayTao = ngnhap;
            pn.TongTien = 0;
            pn.PhieuNhapSanPham = CTPN;
            _context.Add(pn);
            _context.SaveChanges();
            return Json("Thêm thành công");
        }
        // GET: KhuyenMai
        public async Task<IActionResult> Create()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            PopulateCategoryDropDownList();
            PopulateVendorDropDownList();
            return View();
        }

        // GET: KhuyenMai/Details/5
        public async Task<IActionResult> Details(int id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var pn = _context.PhieuNhap.Include("NhaCungCap").Where(pn => pn.PhieuNhapId==id);
            var ctpn = (from ct in _context.PhieuNhapSanPham
                        join sp in _context.SanPham on ct.SanPhamId equals sp.SanPhamId
                        where ct.PhieuNhapId == id
                        select new SPNhap
                        {
                            ID = sp.SanPhamId,
                            Name = sp.TenSanPham,
                            SL = ct.SoLuong,
                            GIA = ct.GiaGoc
                        });
            ViewBag.TTPN = pn;
            ViewBag.CTPN = ctpn;
            return View();
        }

        // GET: KhuyenMai/Create
        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            var DSPN = _context.PhieuNhap.Include("NhaCungCap");
            return View(DSPN);
        }
        // GET: KhuyenMai/Edit/5
        public ActionResult Delete(int id)
        {
            var ctpn = from d in _context.PhieuNhapSanPham
                       where d.PhieuNhapId == id
                       select d;
            foreach (var item in ctpn)
            {
                _context.PhieuNhapSanPham.Remove(item);
            }
            _context.SaveChanges();
            var pn = _context.PhieuNhap.Find(id);
            _context.PhieuNhap.Remove(pn);
            _context.SaveChanges();
            return Json("Xóa phiếu nhập thành công");
        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var ctpn = from d in _context.PhieuNhapSanPham
                           where d.PhieuNhapId == id
                           select d;
                foreach (var item in ctpn)
                {
                    _context.PhieuNhapSanPham.Remove(item);
                }
                _context.SaveChanges();
                var km = _context.PhieuNhap.Find(id);
                _context.PhieuNhap.Remove(km);
            }
            _context.SaveChanges();
            return Json("Tất cả các phiếu nhập được chọn đã được xóa thành công!");
        }

    }
}
