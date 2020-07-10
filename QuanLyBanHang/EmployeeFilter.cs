﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Kiểm tra đúng là tài khoản của admin và không phải client thì được vào
namespace QuanLyBanHang
{
    public class EmployeeFilter : IActionFilter
    {
        private readonly QuanLyBanHangDbContext _context;

        public EmployeeFilter(QuanLyBanHangDbContext context)
        {
            _context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionUser = context.HttpContext.Session.GetString("sessionUser");
            if (sessionUser != null)
            {
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(context.HttpContext.Session.GetString("sessionUser"));
                TaiKhoan taiKhoan = _context.TaiKhoan.Include("VaiTro").Where(tk=>tk.Username==taiKhoanSession.Username).FirstOrDefault();
                if (taiKhoan.VaiTro.VaiTroId == 2)
                {
                   
                }
                else
                {
                    context.Result = new RedirectResult("/Prohibit");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Prohibit");

            }
            
        }

  
    }
}