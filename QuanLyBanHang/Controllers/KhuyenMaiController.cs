using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using QuanLyBanHang.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Http;

namespace QuanLyBanHang.Controllers
{
    [ServiceFilter(typeof(AdminLoginFilter))]
    public class KhuyenMaiController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;
        public KhuyenMaiController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        private void PopulateCategoryDropDownList(object selectedCategory = null)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            var CategoryQuery = from d in _context.PhanLoai
                                where d.NhomLoai != null && d.NhomLoai > 1 && d.NhomLoai < 5
                                orderby d.PhanLoaiId
                                select d;
            ViewBag.PhanLoaiId = new SelectList(CategoryQuery.AsNoTracking(), "PhanLoaiId", "Loai", selectedCategory);
        }

        public ActionResult GetProduct(int id)
        {
            var listProduct = from d in _context.SanPham
                                where d.PhanLoaiId == id
                                select d;
            var s = "<option value =''>Chọn sản phẩm</option>";
            foreach (var item in listProduct)
            {
                s += "<option value =" + item.SanPhamId + ">"+item.TenSanPham+"</option>";
            }
            return Json(s);
        }
        public ActionResult ThemKM(string tenKM, string moTa,DateTime bd, DateTime kt,List<SPKM> listKM )
        {  
            //List<SPKM> listSP = JsonConvert.DeserializeObject<List<SPKM>>(listKM);  
            List<ChiTietKhuyenMai> CTKM = new List<ChiTietKhuyenMai>();
            foreach (var item in listKM) {
                var ctkm = new ChiTietKhuyenMai();
                ctkm.SanPhamId = item.ID;
                ctkm.PhanTramGiam = item.SL;
                CTKM.Add(ctkm);
            }
            var km = new KhuyenMai();
            km.TenKhuyenMai = tenKM;
            km.MoTa = moTa;
            km.NgayBatDau = bd;
            km.NgayKetThuc = kt;
            km.ChiTietKhuyenMai = CTKM;
            _context.Add(km);
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
            return View();
        }
        // GET: KhuyenMai/Details/5
        public async Task<IActionResult> Details(int id)
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            var km = from d in _context.KhuyenMai
                       where d.KhuyenMaiId == id
                       select d;
            var ctkm = (from ct in _context.ChiTietKhuyenMai
                        join sp in _context.SanPham on ct.SanPhamId equals sp.SanPhamId
                        where ct.KhuyenMaiId == id
                        select new SPKM
                        {
                            ID = sp.SanPhamId,
                            Name = sp.TenSanPham,
                            SL = ct.PhanTramGiam
                        });  
            ViewBag.TTKM = km;
            ViewBag.CTKM = ctkm;
            return View();
        }

        // GET: KhuyenMai/Create
        public async Task<IActionResult> Index()
        {
            TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            ViewBag.UserName = taiKhoanSession.Username;
            ViewBag.VaiTroSession = HttpContext.Session.GetInt32("VaiTroSession");
            return View(await _context.KhuyenMai.ToListAsync());
        }
        // GET: KhuyenMai/Edit/5
        public ActionResult Delete(int id)
        {
            var ctkm = from d in _context.ChiTietKhuyenMai
                       where d.KhuyenMaiId == id
                       select d;
            foreach (var item in ctkm)
            {
                _context.ChiTietKhuyenMai.Remove(item);
            }
            _context.SaveChanges();
            var km = _context.KhuyenMai.Find(id);
            _context.KhuyenMai.Remove(km);
            _context.SaveChanges();
            return Json("Xóa khuyến mãi thành công");
        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var ctkm = from d in _context.ChiTietKhuyenMai
                           where d.KhuyenMaiId == id
                           select d;
                foreach (var item in ctkm)
                {
                    _context.ChiTietKhuyenMai.Remove(item);
                }
                _context.SaveChanges();
                var km = _context.KhuyenMai.Find(id);
                _context.KhuyenMai.Remove(km);
            }
            _context.SaveChanges();
            return Json("All the selected voucher deleted successfully!");
        }

    }
}
