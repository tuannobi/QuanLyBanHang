using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class EmailTestController : Controller
    {
        private IEmailService emailService;

        public EmailTestController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public string Index()
        {
            EmailMessage emailMessage = new EmailMessage();
            //
            EmailAddress emailAddress1 = new EmailAddress();
            emailAddress1.Name="TranAnhTuan";
            emailAddress1.Address = "dinhvantien12061998@gmail.com";
            //
            //List<EmailAddress> listEmailAdress1 = new List<EmailAddress>();
            //listEmailAdress1.Add(emailAddress1);
            //emailMessage.FromAddresses = listEmailAdress1;
            EmailAddress emailAddress2 = new EmailAddress();
            //
            emailAddress2.Name = "TranAnhTuan";
            emailAddress2.Address = "17521224@gm.uit.edu.vn";
            List<EmailAddress> listEmailAdress2 = new List<EmailAddress>();
            //listEmailAdress1.Add(emailAddress2);
            emailMessage.ToAddresses = listEmailAdress2 ;
            //
            emailMessage.Subject = "<h1>Hello uit</h1>";
            emailMessage.Content = "<h1>ahsjfsakdfl</h1>";

           

            emailService.Send(emailMessage);
            
            return "Thành công";
        }
    }
}