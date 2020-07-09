using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class CategoryController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public CategoryController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? id)
        {
            //Tạo session list sản phẩm
            List<SanPham> sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == id).ToList();
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            //
            var ThuongHieuList = context.SanPham.Select(sp => sp.ThuongHieu).Distinct().ToList();
            Console.WriteLine(ThuongHieuList.Count());
            //var productList = context.SanPham.Where(sp => sp.PhanLoaiId == id);
            var productList = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoaiId == id && sp.ChiTietKhuyenMai.All(km => km.KhuyenMai.NgayBatDau <= DateTime.Now && km.KhuyenMai.NgayKetThuc >= DateTime.Now)).Union(context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.PhanLoaiId == id && sp.ChiTietKhuyenMai.Any(km => km.KhuyenMai.NgayBatDau >= DateTime.Now || km.KhuyenMai.NgayKetThuc <= DateTime.Now)));
            ViewBag.productList = productList;
            ViewBag.ThuongHieuList = ThuongHieuList;
            return View();
        }

        public IActionResult NhomSanPham(int? id)
        {
            List<SanPham> sanPhams = null;
            switch (id)
            {
                case 111:
                    sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == 9 || sp.PhanLoaiId == 10 || sp.PhanLoaiId == 11).ToList();
                    break;
                case 222:
                    sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == 12 || sp.PhanLoaiId == 13 || sp.PhanLoaiId == 14).ToList();
                    break;
                case 333:
                   sanPhams = context.SanPham.Include("ChiTietKhuyenMai").
                        Where(sp=>sp.ChiTietKhuyenMai.All(km => km.KhuyenMai.NgayBatDau <= DateTime.Now && km.KhuyenMai.NgayKetThuc >= DateTime.Now)).ToList();

                    break;

            }
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            //
            var ThuongHieuList = context.SanPham.Select(sp => sp.ThuongHieu).Distinct().ToList();
            Console.WriteLine(ThuongHieuList.Count());
            var productList = sanPhams;
            ViewBag.productList = productList;
            ViewBag.ThuongHieuList = ThuongHieuList;
            return View("Index");
        }

        public IActionResult NhomLoai(int? id)
        {
            //Tạo session list sản phẩm
            Console.WriteLine(id);
            List<SanPham> sanPhams = null;
            switch (id)
            {
                case 2:
                    sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == 9 || sp.PhanLoaiId == 10 || sp.PhanLoaiId == 11).ToList();
                    break;
                case 3:
                    sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == 12 || sp.PhanLoaiId == 13 || sp.PhanLoaiId == 14).ToList();
                    break;
                case 4:
                    sanPhams = context.SanPham.Where(sp => sp.PhanLoaiId == 15 || sp.PhanLoaiId == 16).ToList();
                    break;

            }
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            //
            var ThuongHieuList = context.SanPham.Select(sp => sp.ThuongHieu).Distinct().ToList();
        Console.WriteLine(ThuongHieuList.Count());
            var productList = sanPhams;
        ViewBag.productList = productList;
            ViewBag.ThuongHieuList = ThuongHieuList;
            return View("Index");

    }

        public IActionResult FilterThuongHieu(string ThuongHieu)
        {
            var sanPhamListSession = HttpContext.Session.GetString("sanPhamListSession");
            List<SanPham> sanPhams = null;
            if (sanPhamListSession != null)
            {
                sanPhams = JsonConvert.DeserializeObject<List<SanPham>>(sanPhamListSession);
            }
            sanPhams = sanPhams.Where(sp => sp.ThuongHieu == ThuongHieu).ToList();
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            return Json(sanPhams);
        }

        public IActionResult FilterThuTu(string SapXep)
        {
            var sanPhamListSession = HttpContext.Session.GetString("sanPhamListSession");
            List<SanPham> sanPhams = null;
            if (sanPhamListSession != null)
            {
                sanPhams = JsonConvert.DeserializeObject<List<SanPham>>(sanPhamListSession);
            }
            if (SapXep != null)
            {
                switch (SapXep){
                    case "default-0":
                        break;
                    case "price-asc":
                        sanPhams = sanPhams.OrderBy(sp => sp.GiaBanLe).ToList();
                        break;
                    case "price-desc":
                        sanPhams = sanPhams.OrderByDescending(sp => sp.GiaBanLe).ToList();
                        break;
                    case "name-asc":
                        sanPhams = sanPhams.OrderBy(sp => sp.TenSanPham).ToList();
                        break;
                    case "name-desc":
                        sanPhams = sanPhams.OrderByDescending(sp => sp.TenSanPham).ToList();
                        break;
                }
            }
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            return Json(sanPhams);
        }

        public IActionResult FilterPrice(int MucGia)
        {
            var sanPhamListSession = HttpContext.Session.GetString("sanPhamListSession");
            List<SanPham> sanPhams = null;
            if (sanPhamListSession != null)
            {
                sanPhams = JsonConvert.DeserializeObject<List<SanPham>>(sanPhamListSession);
            }
            //
            switch (MucGia)
            {
                case 1:
                    sanPhams = sanPhams.Where(sp => sp.GiaBanLe<100000).ToList();
                    break;
                case 2:
                    sanPhams = sanPhams.Where(sp => sp.GiaBanLe >= 100000 && sp.GiaBanLe<300000).ToList();
                    break;
                case 3:
                    sanPhams = sanPhams.Where(sp => sp.GiaBanLe >= 300000 && sp.GiaBanLe < 500000).ToList();
                    break;
                case 4:
                    sanPhams = sanPhams.Where(sp => sp.GiaBanLe >= 500000 && sp.GiaBanLe < 1000000).ToList();
                    break;
                case 5:
                    sanPhams = sanPhams.Where(sp => sp.GiaBanLe >=1000000).ToList();
                    break;
            }
            HttpContext.Session.SetString("sanPhamListSession", JsonConvert.SerializeObject(sanPhams));
            return Json(sanPhams);
        }

    }
}