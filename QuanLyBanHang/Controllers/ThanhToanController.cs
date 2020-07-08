using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using System.Text;
using System.Security.Cryptography;
using QuanLyBanHang.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyBanHang.Controllers
{
    public class ThanhToanController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        private IEmailService emailService;

        public ThanhToanController(QuanLyBanHangDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            KhachHang khachHang = null;
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            int soLuong = 0;
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                soLuong = chiTietHoaDons.Count();
                //2 trường hợp là người dùng đã có tài khoản đặt mua và người dùng không cần đăng nhập để đặt mua
                if (sessionUser != null) //Tức là có phiên đăng nhập
                {
                    Console.OutputEncoding = Encoding.Unicode;
                    Console.InputEncoding = Encoding.Unicode;
                    Console.WriteLine("Hiện có phiên đăng nhập");
                    TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                    khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                    Console.WriteLine("Email cuar khach hang la: " + khachHang.Email);
                }
                var sanPhams = context.SanPham;
                var phiShips = context.PhiShip;
                float tongTienTamTinh = 0;
                foreach (ChiTietHoaDon ctdh in chiTietHoaDons)
                {
                    tongTienTamTinh += (float)ctdh.TongTien;
                }
                Console.WriteLine(sanPhams.Count());
                ViewBag.sanPhamList = sanPhams;
                ViewBag.phiShipList = phiShips;
                ViewBag.tongTienTamTinh = tongTienTamTinh;
                ViewBag.khachHang = khachHang;
                ViewBag.soLuong = soLuong;
                return View(chiTietHoaDons);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string SoNha, int quan, float tongTienTamTinh, float tongTienThanhToan,string GhiChu,
            string HoTen, string Email, string SoDienThoai)
        {
            //2 trường hợp là người dùng đã có tài khoản đặt mua và người dùng không cần đăng nhập để đặt mua
            var sessionUser = HttpContext.Session.GetString("sessionUser");
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            HoaDon hoaDon = new HoaDon();
            if (sessionUser != null && gioHangSession != null) //Tức là có phiên đăng nhập
            {
                HttpContext.Session.SetString("KiemTraKhachHang", "KHC"); //Kiểm tra cập nhật hay thêm mới
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(HttpContext.Session.GetString("sessionUser"));
                KhachHang khachHang = context.KhachHang.Where(kh => kh.TaiKhoan.Username == taiKhoanSession.Username).FirstOrDefault();
                PhiShip phiShip = context.PhiShip.Where(ps => ps.PhiShipId == quan).FirstOrDefault();
                hoaDon.PhuongThucThanhToan = "SHIPCODE";
                hoaDon.SoNha = SoNha;
                hoaDon.Quan = phiShip.Quan;
                hoaDon.PhiShipId = quan;
                hoaDon.KhachHangId = khachHang.KhachHangId;
                hoaDon.TongTien = tongTienTamTinh;
                hoaDon.TongTienThanhToan = tongTienThanhToan;
                hoaDon.GhiChu = GhiChu;
                hoaDon.TrangThai = "Chờ xử lý";
                hoaDon.ThoiGianChoXuLy = DateTime.Now;
                hoaDon.ChiTietHoaDon = chiTietHoaDons;
                //

            }
            else if( sessionUser==null && gioHangSession!=null)//Không có phiên đăng nhập
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                PhiShip phiShip = context.PhiShip.Where(ps => ps.PhiShipId == quan).FirstOrDefault();
                //Kiểm tra người dùng đã từng mua hàng chưa
                KhachHang khachHang = null;
                KhachHang tempKhachHang = context.KhachHang.Include("TaiKhoan").Where(kh => kh.Email == Email).FirstOrDefault();
                if (tempKhachHang != null)
                {
                    //Kiểm tra có phải khách hàng đã có tài khoản hay chưa.. Thanh viên hay đã đặt trước nhưng chưa đăng ký thành viên
                    if (tempKhachHang.TaiKhoan == null)
                    {
                        khachHang = tempKhachHang;
                        HttpContext.Session.SetString("KiemTraKhachHang", "KHC");
                    }
                    else
                    {
                        HttpContext.Session.SetString("messageSession", "Khách hàng đã có tài khoản vui lòng đăng nhập");
                        return Redirect("/login");

                    }
                }
                else
                {
                    khachHang = new KhachHang();
                    khachHang.HoTen = HoTen;
                    khachHang.Email = Email;
                    HttpContext.Session.SetString("KiemTraKhachHang", "KHM");
                }
                khachHang.Sdt = SoDienThoai;
                khachHang.DiaChi = SoNha;
                hoaDon.PhuongThucThanhToan = "SHIPCODE";
                hoaDon.SoNha = SoNha;
                hoaDon.Quan = phiShip.Quan;
                hoaDon.PhiShipId = quan;
                hoaDon.KhachHang = khachHang ;
                hoaDon.TongTien = tongTienTamTinh;
                hoaDon.TongTienThanhToan = tongTienThanhToan;
                hoaDon.GhiChu = GhiChu;
                hoaDon.TrangThai = "Chờ xử lý";
                hoaDon.ThoiGianChoXuLy = DateTime.Now;
                hoaDon.ChiTietHoaDon = chiTietHoaDons;
                //
            }
            string MaXacNhan = TokenGenerator.GenerateToken();
            Console.WriteLine("Ma xac nhan: " + MaXacNhan);
            HttpContext.Session.SetString("MaXacNhanSession", MaXacNhan);
            HttpContext.Session.SetString("hoaDonSession", JsonConvert.SerializeObject(hoaDon));

            HttpContext.Session.SetString("EmailSession", Email);
            HttpContext.Session.SetString("HoTenSession", HoTen);
            sendEmail(Email, HoTen, MaXacNhan);
            return Redirect("/XacNhanDonHang/Index");
        }

       

        public void sendEmail(string destinationEmail, string customerName, string MaXacNhan)
        {
            EmailMessage emailMessage = new EmailMessage();
            //
            EmailAddress emailAddress1 = new EmailAddress();
            emailAddress1.Name = "Email xác nhận từ shop bán mỹ phẩm";
            emailAddress1.Address = "dinhvantien12061998@gmail.com";
            //
            List<EmailAddress> listEmailAdress1 = new List<EmailAddress>();
            listEmailAdress1.Add(emailAddress1);
            emailMessage.FromAddresses = listEmailAdress1;
            EmailAddress emailAddress2 = new EmailAddress();
            //
            emailAddress2.Name = customerName;
            emailAddress2.Address = destinationEmail;
            List<EmailAddress> listEmailAdress2 = new List<EmailAddress>();
            listEmailAdress2.Add(emailAddress2);
            emailMessage.ToAddresses = listEmailAdress2;
            //
            emailMessage.Subject = "Thư xác nhận đơn hàng đã đặt mua";
            emailMessage.Content = "<h1>Xin chào "+customerName+"</h1><h1> Mã xác nhận của bạn là: "+ MaXacNhan + "</h1>";

            emailService.Send(emailMessage);
        }
    }
}