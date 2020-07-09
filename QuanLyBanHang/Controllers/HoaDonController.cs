using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;


namespace QuanLyBanHang.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public HoaDonController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        // GET: HoaDon
        public IActionResult Index()
        {
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.TrangThai == "Chờ xử lý");
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                 join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                 join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                 where hd.TrangThai == "Chờ xử lý"
                                 select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
            ViewBag.TTHD = thongtinhoadon;
            return View();
        }

        public IActionResult Index1()
        {
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.TrangThai == "Đã xử lý");
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.TrangThai == "Đã xử lý"
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
            ViewBag.TTHD = thongtinhoadon;
            return View();
        }

        public IActionResult Details(int id)
        {
            var thongtinsanpham = _context.ChiTietHoaDon.Include("SanPham").Where(sp => sp.HoaDonId == id);
            /*var thongtinsanpham = (from sp in _context.SanPham
                                  join cthd in _context.ChiTietHoaDon on sp.SanPhamId equals cthd.SanPhamId
                                  where cthd.HoaDonId == id
                                  select new ThongTinHoaDon
                                  {
                                      SanPham = sp,
                                      ChiTietHoaDon = cthd
                                  });*/
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.HoaDonId == id);
            /*var thongtinhoadon = (from hd in _context.HoaDon
                                  join kh in _context.KhachHang on hd.KhachHangId equals kh.KhachHangId
                                  join ship in _context.PhiShip on hd.PhiShipId equals ship.PhiShipId
                                  where hd.HoaDonId == id
                                  select new ThongTinHoaDon
                                  {
                                      HoaDon = hd,
                                      PhiShip = ship,
                                      KhachHang = kh
                                  });*/
            ViewBag.TTHD = thongtinhoadon;
            ViewBag.TTSP = thongtinsanpham;
            return View();
        }

        [HttpPost]
        public ActionResult Duyet(int id)
        {
            var hd = _context.HoaDon.Find(id);
            hd.TrangThai = "Đã xử lý";
            hd.ThoiGianDaXuLy = DateTime.Now;
            _context.Update(hd);
            _context.SaveChanges();
            return Json("Hóa đơn này đã duyệt thành công");
        }

        [HttpPost]
        public ActionResult DuyetSelected(int[] selected)
        {
            foreach (int id in selected)
            {
                var hd = _context.HoaDon.Find(id);
                hd.TrangThai = "Đã xử lý";
                hd.ThoiGianDaXuLy = DateTime.Now;
                _context.Update(hd);
            }
            _context.SaveChanges();
            return Json("Tất cả những hóa đơn được chọn đã duyệt thành công");
        }

       

        [HttpPost]
        public IActionResult ThongKeHoaDonTheoThoiGian(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            Console.WriteLine(ngayBatDau);
            Console.WriteLine(ngayKetThuc);
            var hoaDons = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.ThoiGianDaXuLy >= ngayBatDau && hd.ThoiGianDaXuLy <= ngayKetThuc).ToList();
            ViewBag.TTHD = hoaDons;
            HttpContext.Session.SetString("ngayBatDauSession", JsonConvert.SerializeObject(ngayBatDau));
            HttpContext.Session.SetString("ngayKetThucSession", JsonConvert.SerializeObject(ngayKetThuc));
            return View("Index1");
        }

        public IActionResult InDanhSachHoaDon()
        {
            DateTime ngayBatDau = JsonConvert.DeserializeObject<DateTime>(HttpContext.Session.GetString("ngayBatDauSession"));
            DateTime ngayKetThuc = JsonConvert.DeserializeObject<DateTime>(HttpContext.Session.GetString("ngayKetThucSession"));
            var hoaDons = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.ThoiGianDaXuLy >= ngayBatDau && hd.ThoiGianDaXuLy <= ngayKetThuc).ToList();
            //Chỗ này xử lý convert html trả về pdf
            MemoryStream workStream = new MemoryStream();
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 25, 25);
            string html = "<html><body> < div style = 'text-align:center;>" +
                    "<h2 style='font-size:20px'>Shop Beauty</h2>" +
                    "<p>Kí túc xá khu B DHQG TP.Hồ Chí Minh</p></div>" +
                    " <table style = 'text-align: center; border-collapse: collapse;' > " +
            " <tr style = 'border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;' > " +
                "<th> Số hóa đơn</th>" +
                  "<th> Ngày tạo</th>" +
                    "<th>Khách hàng</th>" +
                      "<th>Địa chỉ</th>" +
                        "<th>Phí ship</th>" +
                          "<th>Tổng tiền</th>" +
                            "</tr>";
            string chitiet = "";
           
            var doanhthu = 0;
            foreach (var item in hoaDons)
            {
                string html1 = "<tr>" +
                    "<td>" + item.HoaDonId + "</td>" +
                    "<td>" + item.ThoiGianChoXuLy + "</td>" +
                    "<td>" + item.KhachHang.HoTen + "</td>" +
                    "<td>" + item.SoNha + " - " + item.Quan + "</td>" +
                    "<td>" + item.PhiShip.ChiPhi + "</td>" +
                    "<td>" + item.TongTienThanhToan +"</td>" +
                    "</tr>";
                
                doanhthu = (int)(doanhthu + item.TongTienThanhToan);
                chitiet = chitiet + html1;
            }
            Console.WriteLine(doanhthu);
            string dthu = "<tr><td colspan='5' style='float:right'>Tổng doanh thu:</td><td>" + doanhthu + "</td></tr>";
            string html3="</table></body></html>";
            html = html + chitiet + dthu+html3;
            byte[] byteInfo = GetPDF(html);
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }
        public ActionResult InHoaDon(int id)
        {
            Console.WriteLine(id);
            var hoaDon = _context.HoaDon.Include("ChiTietHoaDon").Include("KhachHang").Where(hd => hd.HoaDonId == id).FirstOrDefault();
            Console.WriteLine(hoaDon.PhiShip);
            var thongtinsanpham = _context.ChiTietHoaDon.Include("SanPham").Where(sp => sp.HoaDonId == id).ToList();
           
            var thongtinhoadon = _context.HoaDon.Include("KhachHang").Include("PhiShip").Where(hd => hd.HoaDonId == id).ToList();
          

            MemoryStream workStream = new MemoryStream();
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 25, 25, 25, 25);
            //    string html =
            //        "<div style='text-align:center;>" +
            //        "<h2 style='font-size:20px'>Shop Beauty</h2>" +
            //        "<p>Kí túc xá khu B DHQG TP.Hồ Chí Minh</p></div>" +
            //        "  <div class=''>" +
            //       " < span class='span1'>Số hóa đơn: </span>" +
            //         " <span id = 'sohoadon' class='span2'>" + hoaDon.HoaDonId + "</span>" +
            //     " </div>" +
            //      "<div class=''>" +
            //       "   <span class='span1'>Ngày:</span>" +
            //        "  <span class='span2'> " + hoaDon.ThoiGianChoXuLy + "</span>" +
            //     " </div>" +
            //     " <div class=''>" +
            //        "  <span class='span1'>Khách hàng: </span>" +
            //         " <span class='span2'>" + hoaDon.KhachHang.HoTen + "</span>" +
            //    "  </div>" +
            //    "  <div class=''>" +
            //        "  <span class='span1'>Địa chỉ: </span>" +
            //        "  <span class='span2'>" + hoaDon.SoNha + ", " + hoaDon.Quan + "</span>" +
            //     " </div>" +
            //     " <div class=''>" +
            //         " <span class='span1'>Số điện thoại: </span>" +
            //         " <span class='span2'>" + hoaDon.KhachHang.Sdt + "</span>" +
            //         " </div>" +
            //         " <div class=''>" +
            //            "  <span class='span1'> Chi tiết hóa đơn:</span>" +
            //         " </div>" +
            //         " <br />" +

            //        "< div >" +
            //" <table style = 'text-align: center; border-collapse: collapse;' > " +
            //" <tr style = 'border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;' > " +
            //    "<th>  Số thứ tự</th>" +
            //      "<th>  Tên sản phẩm</th>" +
            //        "<th>  Số lượng</th>" +
            //          "<th>  Đơn giá</th>" +
            //            "<th>  Thành tiền</th>" +
            //              "<th>  Tiền khuyến mã</th>" +
            //                "<th>  Tổng tiền</th>" +
            //              "</tr>";
            string html = @"
<h1>Hello world</h1>
<hr style='border-top: 1px solid red;' />
";





       //                   int i = 1;
       //     string chitiet = "";
       //     foreach (var item in thongtinsanpham)
       //     {
       //         Console.WriteLine(item.SanPham.TenSanPham);
       //         var thanhtien = item.TongTien - item.TienKhuyenMai;
       //         var dongia = thanhtien / item.SoLuong;
       //         string table = "<tr style ='border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;'>" +
       //                 "  <td style = 'text-align: center;'> " + i + "</td>" +
       //                  "  <td style = 'text-align: center;'> " + item.SanPham.TenSanPham + "</td>" +
       //                    " <td style = 'text-align: center;'>" + item.SoLuong + "</td>" +
       //                     " <td style = 'text-align: center;'>" + dongia + "</td>" +
       //                         "<td style = 'text-align: center; >" + thanhtien + "</td>" +
       //                       " <td style = 'text-align: center;'>" + item.TienKhuyenMai + " </td>" +
       //                         " <td style = 'text-align: center;'>" + item.TongTien + " </td>" +
       //                     " </tr>";
       //         i = i + 1;
       //         Console.WriteLine(dongia);
       //         chitiet = chitiet + table;
       //     }
       //     string tthd = "";
       //     foreach (var item in thongtinhoadon )
       //     {
       //          tthd = "  <tr style = 'border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;' >" +
                   
       //             "  <td colspan = '6' style='float:right' >< center >< b > Tổng tiền:</ b ></ center ></ td >" +
       //                   "     <td colspan = '6' style='float:right' >< center >< b >" + item.TongTien + " </b ></center ></td>" +
       //                    " </tr >" +
       //                   "  <tr style = 'border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;'>" +
                             
       //                         "<td colspan = '2' >< center >< b > Phí Ship:</ b ></ center ></ td >" +
       //                           "       <td colspan = '2' >< center >< b >"+item.PhiShip.ChiPhi+" </ b ></center ></td >" +
       //                          "     </tr >" +
       //                             "  <tr style = 'border-bottom: 2px solid #2f2d2d42;margin-bottom: 3px;'>" +

       //                                "   <td colspan = '6' style='float:right' ><center ><b > Tổng tiền thanh toán:</b ></ center >< td >" +
       //                                 "           <td colspan = '2' ><center ><b > "+item.TongTienThanhToan +"</b ></center ></td >" +
       //                                  "       </tr>";
               
       //     }; 
       //string tableket = "</table>";
            //html = html + chitiet+tthd + tableket;
            byte[] byteInfo = GetPDF(html);
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            return new FileStreamResult(workStream, "application/pdf");
        }

        [HttpPost]
       
       


        public byte[] GetPDF(string pHTML)
        {
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();
            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

            bPDF = ms.ToArray();

            return bPDF;
        }
    }

   


}
