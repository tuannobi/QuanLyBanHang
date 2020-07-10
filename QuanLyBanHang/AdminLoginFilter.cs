using Microsoft.AspNetCore.Http;
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
    public class AdminLoginFilter : IActionFilter
    {
        private readonly QuanLyBanHangDbContext _context;

        public AdminLoginFilter(QuanLyBanHangDbContext context)
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
                
            }
            else
            {
                context.Result = new RedirectResult("/Login");

            }
            
        }

  
    }
}
