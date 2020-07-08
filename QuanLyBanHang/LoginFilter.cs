using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHang
{
    public class LoginFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionUser = context.HttpContext.Session.GetString("sessionUser");
            if (sessionUser != null)
            {
                TaiKhoan taiKhoanSession = JsonConvert.DeserializeObject<TaiKhoan>(context.HttpContext.Session.GetString("sessionUser"));

            }
            else
            {
                throw new Exception("Login before");
            }

        }

  
    }
}
