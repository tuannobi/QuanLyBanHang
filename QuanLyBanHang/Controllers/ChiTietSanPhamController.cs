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
    public class ChiTietSanPhamController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public ChiTietSanPhamController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index(int? id)
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            int soLuong = 0;
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
                ViewBag.chiTietHoaDons = chiTietHoaDons;
            }
            var binhLuans = context.BinhLuan.Include("Reply").Include("KhachHang").Where(sp => sp.SanPhamId == id).OrderByDescending(sp=>sp.ThoiGian).ToList();
            var repLies = context.Reply.Include("KhachHang").OrderByDescending(rp=>rp.ThoiGian).ToList();
            var sanPham = context.SanPham.Where(sp => sp.SanPhamId == id).FirstOrDefault();
            ViewBag.sanPham = sanPham;
            ViewBag.soLuong = soLuong;
            ViewBag.binhLuanList = binhLuans;
            ViewBag.replyList = repLies;
            return View(sanPham);
        }

        public IActionResult Comment(string NoiDung,int SanPhamId)
        {
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            if (sessionUser != null) //Tức là có phiên đăng nhập
            {
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                KhachHang khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                BinhLuan binhLuan = new BinhLuan();
                binhLuan.NoiDung = NoiDung;
                binhLuan.ThoiGian = System.DateTime.Now;
                binhLuan.KhachHangId = khachHang.KhachHangId;
                binhLuan.SanPhamId = SanPhamId;
                context.Add(binhLuan);
                context.SaveChanges();
                return Json(binhLuan);
            }
            else
            {
                return Json("Đăng nhập để bình luận");
            }
        }

        public IActionResult Reply(string NoiDung, int BinhLuanId)
        {
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            if (sessionUser != null) //Tức là có phiên đăng nhập
            {
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                KhachHang khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                Reply reply = new Reply();
                reply.NoiDung = NoiDung;
                reply.ThoiGian = System.DateTime.Now;
                reply.BinhLuanId = BinhLuanId;
                reply.KhachHangId = khachHang.KhachHangId;
                context.Add(reply);
                context.SaveChanges();
                return Json(reply);
            }
            else
            {
                return Json("Đăng nhập để trả lời bình luận bình luận");
            }
        }
    }
}