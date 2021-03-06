﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var trangDiemSanPhams = from s in context.SanPham
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
            var chiTietKhuyenMais = from km in context.ChiTietKhuyenMai
                                    select km;
            int soLuong=0;
            if (sessionUser != null)
            {
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                var username = taiKhoanSession.Username;               
                ViewBag.username = username;   
            }
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
            }
            ViewBag.soLuong = soLuong;
            ViewBag.chiTietKhuyenMais = chiTietKhuyenMais;
            ViewBag.trangDiemSanPhams = trangDiemSanPhams;
            ViewBag.chamSocDaSanPhams = chamSocDaSanPhams;
            ViewBag.nuocHoaSanPhams = nuocHoaSanPhams;
            return View();
        }
    }
}