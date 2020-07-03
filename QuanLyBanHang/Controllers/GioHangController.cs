using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyBanHang.Controllers
{
    public class GioHangController : Controller
    {
        private readonly QuanLyBanHangDbContext context;

        public GioHangController(QuanLyBanHangDbContext context)
        {
            this.context = context;
        }

        public IActionResult Details()
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            int soLuong = 0;
            if (gioHangSession != null)
            {
                var sanPhams = context.SanPham;
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                float tongTienThanhToan = 0;
                foreach(ChiTietHoaDon ctdh in chiTietHoaDons)
                {
                    tongTienThanhToan +=(float) ctdh.TongTien;
                }
                Console.WriteLine(sanPhams.Count());
                soLuong=chiTietHoaDons.Count();
                ViewBag.soLuong = soLuong;
                ViewBag.sanPhamList = sanPhams;
                ViewBag.tongTienThanhToan = tongTienThanhToan;
                return View(chiTietHoaDons);
            }
            else
            {
                ViewBag.soLuong = soLuong;
                ViewBag.message = "Không có sản phẩm nào";
            }
           return View();
        }

        // GET: /<controller>/
        public string Index(int product_quantity, int id_product)
        {
            Console.WriteLine("Giỏ hàng Ajax");
            int soLuong = product_quantity;
            Console.WriteLine("Số lượng: " + soLuong);
            int productId = id_product;
            Console.WriteLine("Mã sản phẩm: " + id_product);
            float khuyenMai = 0;
            float tongTien = 0;
            int count = 0;
            SanPham sp = context.SanPham.Where(s => s.SanPhamId == productId).FirstOrDefault();
            ChiTietKhuyenMai km = context.ChiTietKhuyenMai.Where(k => k.SanPhamId == productId).FirstOrDefault();
            if (km != null)
            {
                khuyenMai = (float)(sp.GiaBanLe - (sp.GiaBanLe * km.PhanTramGiam));
            }
            else
            {
                khuyenMai = 0;
            }
            Console.WriteLine("Tiền giảm khuyến mãi: " + khuyenMai);
            tongTien = (float)(soLuong * sp.GiaBanLe-khuyenMai);
            Console.WriteLine("Tổng tiền phải trả: " + tongTien);
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            if (gioHangSession == null) //Nếu giỏ hàng chưa được khởi tạo
            {
                List<ChiTietHoaDon> chiTietHoaDons = new List<ChiTietHoaDon>();
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                chiTietHoaDon.SanPhamId = productId;
                chiTietHoaDon.SoLuong = soLuong;
                chiTietHoaDon.TienKhuyenMai = khuyenMai;
                chiTietHoaDon.TongTien = tongTien;
                chiTietHoaDons.Add(chiTietHoaDon);
                count = chiTietHoaDons.Count();
                HttpContext.Session.SetString("gioHangSession", JsonConvert.SerializeObject(chiTietHoaDons));
            }
            else //Giỏ hàng đã có sẵn, người dùng muốn thêm vào
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
                //Kiểm tra sản phẩm đã tồn tại trong list có bị trùng với sản phẩm vừa mới thêm vào
                //Nếu trùng thì gộp lại thành 1
                bool flag = false;
                foreach(ChiTietHoaDon cthd in chiTietHoaDons)
                {
                    if (cthd.SanPhamId == productId) //Tìm thấy có sản phẩm bị trùng
                    {
                        cthd.SoLuong = cthd.SoLuong + soLuong;
                        cthd.TienKhuyenMai = cthd.TienKhuyenMai + khuyenMai;
                        cthd.TongTien = cthd.TongTien + tongTien;
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    chiTietHoaDon.SanPhamId = productId;
                    chiTietHoaDon.SoLuong = soLuong;
                    chiTietHoaDon.TienKhuyenMai = khuyenMai;
                    chiTietHoaDon.TongTien = tongTien;
                    chiTietHoaDons.Add(chiTietHoaDon);
                }
                count = chiTietHoaDons.Count();
                HttpContext.Session.SetString("gioHangSession", JsonConvert.SerializeObject(chiTietHoaDons));
            }
            return "Thêm thành công. Giỏ hàng có "+count;
        }

        public IActionResult Delete(int id)
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                Console.WriteLine(chiTietHoaDons.Count());
                foreach(var cthd in chiTietHoaDons.ToList())
                {
                    if (cthd.SanPhamId == id)
                    {
                        chiTietHoaDons.Remove(cthd);
                        HttpContext.Session.SetString("gioHangSession", JsonConvert.SerializeObject(chiTietHoaDons));
                    }
                }
            }
            return Redirect("/GioHang/Details");
        }

        public IActionResult EditSoLuong(int SanPhamId,int soLuong)
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            if (gioHangSession != null)
            {
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                foreach (var cthd in chiTietHoaDons.ToList())
                {
                    if (cthd.SanPhamId == SanPhamId)
                    {
                        cthd.SoLuong = soLuong;
                        float khuyenMai = 0;
                        ChiTietKhuyenMai km = context.ChiTietKhuyenMai.Where(k => k.SanPhamId == SanPhamId).FirstOrDefault();
                        SanPham sp = context.SanPham.Where(s => s.SanPhamId == SanPhamId).FirstOrDefault();
                        if (km != null)
                        {
                            khuyenMai = (float)(sp.GiaBanLe - (sp.GiaBanLe * km.PhanTramGiam));
                        }
                        cthd.TienKhuyenMai = khuyenMai*soLuong;
                        cthd.TongTien = sp.GiaBanLe * soLuong - khuyenMai * soLuong;
                        HttpContext.Session.SetString("gioHangSession", JsonConvert.SerializeObject(chiTietHoaDons));
                        return Json(cthd);
                    }
                }
            }
            return View();
        }

        public float getTongTien()
        {
            var gioHangSession = HttpContext.Session.GetString("gioHangSession");
            if (gioHangSession != null)
            {
                float tongTien = 0;
                List<ChiTietHoaDon> chiTietHoaDons = JsonConvert.DeserializeObject<List<ChiTietHoaDon>>(HttpContext.Session.GetString("gioHangSession"));
                foreach (var cthd in chiTietHoaDons.ToList())
                {
                    tongTien +=(float) cthd.TongTien;
                }
                return tongTien;
            }
            return 0;
        }

        public float getKho(int SanPhamId)
        {
            var Kho =context.SanPham.Where(sp => sp.SanPhamId == SanPhamId).Select(s => s.Kho).FirstOrDefault();
            return (float)Kho;
        }

        
    }
}
