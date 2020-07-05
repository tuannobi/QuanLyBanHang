using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class HomepageController : Controller
    {

        private readonly QuanLyBanHangDbContext context;

        public HomepageController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            var ngay = DateTime.Now;
            var trangDiemSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 2);
            var chamSocDaSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 3);
            var nuocHoaSanPhams = context.SanPham.Include("PhanLoai").Include("ChiTietKhuyenMai").Where(sp => sp.PhanLoai.NhomLoai == 4);
            /*var trangDiemSanPhams = from s in context.SanPham
                                    join p in context.PhanLoai
                                    on s.PhanLoaiId equals p.PhanLoaiId
                                    where p.NhomLoai == 2
                                    select s;
            var chamSocDaSanPhams = from s in context.SanPham
                                    join p in context.PhanLoai
                                    on s.PhanLoaiId equals p.PhanLoaiId
                                    where p.NhomLoai == 3
                                    select s;
            var nuocHoaSanPhams = from s in context.SanPham
                                  join p in context.PhanLoai
                                  on s.PhanLoaiId equals p.PhanLoaiId
                                  where p.NhomLoai == 4
                                  select s;
            var chiTietKhuyenMais = context.SanPham.Include("ChiTietKhuyenMai");*/
            //var chiTietKhuyenMais = context.SanPham.Include("ChiTietKhuyenMai").Where(sp => (sp.ChiTietKhuyenMai.Where(x => x.KhuyenMai.NgayBatDau >= DateTime.Now)));
            var chiTietKhuyenMais = context.SanPham.Include("ChiTietKhuyenMai").Where(sp=>sp.ChiTietKhuyenMai.Any(km=>km.KhuyenMai.NgayBatDau<=DateTime.Now)).ToList();
            //Console.WriteLine(DateTime.Now);
            //Console.WriteLine(chiTietKhuyenMais.Count());
            foreach (var item in chiTietKhuyenMais)
            {
                if (item.ChiTietKhuyenMai.FirstOrDefault() != null)
                    Console.WriteLine(item.ChiTietKhuyenMai.FirstOrDefault().PhanTramGiam);
            }  

            //int soLuong=0;
            //if (sessionUser != null)
            //{
            //    TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
            //    var username = taiKhoanSession.Username;               
            //    ViewBag.username = username;   
            //}if ma
            //if (gioHangSession != null)
            //{
            //    List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
            //    soLuong = chiTietHoaDons.Count();
            //}
            //ViewBag.soLuong = soLuong;
            //ViewBag.chiTietKhuyenMais = chiTietKhuyenMais;
            ViewBag.trangDiemSanPhams = trangDiemSanPhams;
            ViewBag.chamSocDaSanPhams = chamSocDaSanPhams;
            ViewBag.nuocHoaSanPhams = nuocHoaSanPhams;
            return View();
        }
    }
}