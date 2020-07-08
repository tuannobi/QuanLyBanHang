using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GrapeCity.Documents.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using QuanLyBanHang.Utils;

namespace QuanLyBanHang.Controllers
{
    public class XacNhanDonHangController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        private IEmailService emailService;

        public XacNhanDonHangController(QuanLyBanHangDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string MaxacNhan)
        {
            string MaXacNhanSession = HttpContext.Session.GetString("MaXacNhanSession");

            if (MaxacNhan.Equals(MaXacNhanSession))
            {
                string HoTen = HttpContext.Session.GetString("HoTenSession");
                string Email = HttpContext.Session.GetString("EmailSession");
                string KiemTraKhachHang = HttpContext.Session.GetString("KiemTraKhachHang");
                HoaDon hoaDon = JsonConvert.DeserializeObject<HoaDon>(HttpContext.Session.GetString("hoaDonSession"));
                if (KiemTraKhachHang.Equals("KHM"))
                {
                    context.Add(hoaDon);
                    context.SaveChanges();
                }
                else
                {
                    context.Update(hoaDon);
                    context.SaveChanges();
                }
                sendSuccessfulEmail(Email, HoTen, MaXacNhanSession, hoaDon.HoaDonId);
                HttpContext.Session.Remove("MaXacNhanSession");
                HttpContext.Session.Remove("hoaDonSession");
                HttpContext.Session.Remove("gioHangSession");
                HttpContext.Session.Remove("EmailSession");
                HttpContext.Session.Remove("HoTenSession");
                HttpContext.Session.Remove("KiemTraKhachHang");
                return Redirect("/homepage");
            }
            else
            {
                HttpContext.Session.Remove("MaXacNhanSession");
                return View("FailPage");
            }


        }

        [HttpPost]
        public IActionResult Again()
        {
            string MaXacNhan = TokenGenerator.GenerateToken();
            string HoTen = HttpContext.Session.GetString("HoTenSession");
            string Email = HttpContext.Session.GetString("EmailSession");
            HttpContext.Session.SetString("MaXacNhanSession", MaXacNhan);
            sendEmail(Email, HoTen, MaXacNhan);
            return View("Index");
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
            emailMessage.Content = "<h1>Xin chào " + customerName + "</h1><h1> Mã xác nhận của bạn là: " + MaXacNhan + "</h1>";

            emailService.Send(emailMessage);
        }

        public void sendSuccessfulEmail(string destinationEmail, string customerName, string MaXacNhan, int SoHoaDon)
        {
            List<ChiTietHoaDon> ChiTietHoaDons = context.ChiTietHoaDon.Include("SanPham").Where(hd => hd.HoaDonId == SoHoaDon).ToList();
            HoaDon hoaDon = context.HoaDon.Include("PhiShip").Where(hd => hd.HoaDonId == SoHoaDon).FirstOrDefault();
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
            emailMessage.Subject = "Đặt hàng thành công";
            var content = "";
            content = @"

<table id = 'customers'>
  <th>
    <tr>
      <th> Tên sản phẩm </th>
         <th>Đơn giá</th>
      <th>Số lượng</th>
      <th>Tiền giảm</th>
      <th>Thành tiền</th>
    </tr>
  </th>
  <td>
";
            foreach (ChiTietHoaDon cthd in ChiTietHoaDons)
            {
                content += "<tr><td> " + cthd.SanPham.TenSanPham + "</td><td>" + cthd.SanPham.GiaBanLe + "</td><td>" + cthd.SoLuong + "</td><td>" + cthd.TienKhuyenMai + "</td><td>" + cthd.TongTien + "</td></tr>";
            }
            content += "<tr><td colspan ='4'> Tạm tính </td><td> " + hoaDon.TongTien + " </td></tr><tr><td colspan ='4'> Phí Ship </td><td> " + hoaDon.PhiShip.ChiPhi + " </td></tr><tr><td colspan ='4'> Tổng số tiền phải trả </td><td>" + hoaDon.TongTienThanhToan + " </td></tr>";
            content += "  </td></table> ";
            emailMessage.Content = content;
            emailService.Send(emailMessage);
        }


    }
 }