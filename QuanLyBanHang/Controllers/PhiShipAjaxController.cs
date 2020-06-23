using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class PhiShipAjaxController : Controller
    {
        private readonly QuanLyBanHangDbContext _context;

        public PhiShipAjaxController(QuanLyBanHangDbContext context)
        {
            _context = context;
        }
        public ActionResult Index(int PhiShipId)
        {
            PhiShip phiShip = _context.PhiShip.Where(ps=>ps.PhiShipId==PhiShipId).FirstOrDefault();           
            return Json(phiShip);
        }
    }
}